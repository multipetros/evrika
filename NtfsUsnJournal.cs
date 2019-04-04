/* 
 * Based on NtfsUsnJournal.cs of MTFScanner <http://archive.codeplex.com/?p=mftscanner> 
 * and StCroixSkipper's USN Journal Explorer <www.dreamincode.net/forums/blog/1017-stcroixskippers>
 * and snippet Roland Bogosi's TV-Show Tracker <https://github.com/RoliSoft/RS-TV-Show-Tracker>
 * 
 * In this class variant:
 * - Some unnecessary methods removed
 * - Refactoring to meet my coding style
 * - Win32 errors at construction throwed as Exceptions
 * - GetNtfsVolumeFolders and GetFilesMatchingFilter merged to GetFilesMatchingName, 
 *   providing also options for serach type (see SearchType enum), and exact search,
 *   end search be more efficient.
 * - Support for X64 builds added.
 * - GetFilesMatchingName combined with GetPathFromFileReference functionality,
 *   avoiding the GetPathFromFileReference API calls, and use parallelism to speedup
 *   the results.  
 * 
 *   2019, Petros Kyladitis, for Evrika project
 */
 
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Globalization;
using PInvoke;

namespace UsnJournal{
	public class FileSet{
		private string name ;
		private string path ;
		
		public FileSet(){ }
		
		public FileSet(string name, string path){
			this.name = name ;
			this.path = path ;
		}
		
		public string Name{
			get{ return this.name ; }
			set{ this.name = value ; }
		}
		
		public string Path{
			get{ return this.path ; }
			set{ this.path = value ; }
		}
	}
	
    public class NtfsUsnJournal : IDisposable{
        #region enums
        
		public enum SearchType{
			FILES_FOLDERS = 0,
			FILES_ONLY = 1,
			FOLDERS_ONLY = 2
		} ;
        
        public enum ComparisonType{
        	EXACT_MATCH = 0,
        	CONTAINS = 1,
        	STARTS_WITH = 2,
        	ENDS_WITH = 3
        }
        
        public enum UsnJournalReturnCode{
            INVALID_HANDLE_VALUE = -1,
            USN_JOURNAL_SUCCESS = 0,
            ERROR_INVALID_FUNCTION = 1,
            ERROR_FILE_NOT_FOUND = 2,
            ERROR_PATH_NOT_FOUND = 3,
            ERROR_TOO_MANY_OPEN_FILES = 4,
            ERROR_ACCESS_DENIED = 5,
            ERROR_INVALID_HANDLE = 6,
            ERROR_INVALID_DATA = 13,
            ERROR_HANDLE_EOF = 38,
            ERROR_NOT_SUPPORTED = 50,
            ERROR_INVALID_PARAMETER = 87,
            ERROR_JOURNAL_DELETE_IN_PROGRESS = 1178,
            USN_JOURNAL_NOT_ACTIVE = 1179,
            ERROR_JOURNAL_ENTRY_DELETED = 1181,
            ERROR_INVALID_USER_BUFFER = 1784,
            USN_JOURNAL_INVALID = 17001,
            VOLUME_NOT_NTFS = 17003,
            INVALID_FILE_REFERENCE_NUMBER = 17004,
            USN_JOURNAL_ERROR = 17005
        } ;

        public enum UsnReasonCode{
            USN_REASON_DATA_OVERWRITE = 0x00000001,
            USN_REASON_DATA_EXTEND = 0x00000002,
            USN_REASON_DATA_TRUNCATION = 0x00000004,
            USN_REASON_NAMED_DATA_OVERWRITE = 0x00000010,
            USN_REASON_NAMED_DATA_EXTEND = 0x00000020,
            USN_REASON_NAMED_DATA_TRUNCATION = 0x00000040,
            USN_REASON_FILE_CREATE = 0x00000100,
            USN_REASON_FILE_DELETE = 0x00000200,
            USN_REASON_EA_CHANGE = 0x00000400,
            USN_REASON_SECURITY_CHANGE = 0x00000800,
            USN_REASON_RENAME_OLD_NAME = 0x00001000,
            USN_REASON_RENAME_NEW_NAME = 0x00002000,
            USN_REASON_INDEXABLE_CHANGE = 0x00004000,
            USN_REASON_BASIC_INFO_CHANGE = 0x00008000,
            USN_REASON_HARD_LINK_CHANGE = 0x00010000,
            USN_REASON_COMPRESSION_CHANGE = 0x00020000,
            USN_REASON_ENCRYPTION_CHANGE = 0x00040000,
            USN_REASON_OBJECT_ID_CHANGE = 0x00080000,
            USN_REASON_REPARSE_POINT_CHANGE = 0x00100000,
            USN_REASON_STREAM_CHANGE = 0x00200000,
            USN_REASON_CLOSE = -1
        } ;

        #endregion

        #region private member variables

        private DriveInfo driveInfo = null;
        private uint volumeSerialNumber;
        private IntPtr usnJournalRootHandle;
        private bool bNtfsVolume;

        #endregion

        #region properties

        public string VolumeName{
            get { return this.driveInfo.Name; }
        }

        public long AvailableFreeSpace{
            get { return this.driveInfo.AvailableFreeSpace; }
        }

        public long TotalFreeSpace{
            get { return this.driveInfo.TotalFreeSpace; }
        }

        public string Format{
            get { return this.driveInfo.DriveFormat; }
        }

        public DirectoryInfo RootDirectory{
            get { return this.driveInfo.RootDirectory; }
        }

        public long TotalSize{
            get { return this.driveInfo.TotalSize; }
        }

        public string VolumeLabel{
            get { return this.driveInfo.VolumeLabel; }
        }

        public uint VolumeSerialNumber{
            get { return this.volumeSerialNumber; }
        }
        
        public bool IsX64{
        	get{ return (Marshal.SizeOf(typeof(IntPtr)) == 8) ; }
        }
        

        #endregion

        #region constructor

        /// <summary>
        /// Constructor for NtfsUsnJournal class.  If no exception is thrown, this.usnJournalRootHandle and
        /// this.volumeSerialNumber can be assumed to be good. If an exception is thrown, the NtfsUsnJournal
        /// object is not usable.
        /// </summary>
        /// <param name="driveInfo">DriveInfo object that provides access to information about a volume</param>
        /// <remarks> 
        /// An exception thrown if the volume is not an 'NTFS' volume or
        /// if GetRootHandle() or GetVolumeSerialNumber() functions fail. 
        /// Each public method checks to see if the volume is NTFS and if the this.usnJournalRootHandle is
        /// valid.  If these two conditions aren't met, then the public function will return a UsnJournalReturnCode
        /// error.
        /// </remarks>
        public NtfsUsnJournal(DriveInfo driveInfo){
            this.driveInfo = driveInfo;
            if (this.driveInfo.DriveFormat == "NTFS"){
                bNtfsVolume = true;
                IntPtr rootHandle = IntPtr.Zero;
                UsnJournalReturnCode usnRtnCode = GetRootHandle(out rootHandle);
                if (usnRtnCode == UsnJournalReturnCode.USN_JOURNAL_SUCCESS){
                    this.usnJournalRootHandle = rootHandle;
                    usnRtnCode = GetVolumeSerialNumber(this.driveInfo, out this.volumeSerialNumber);
                    if (usnRtnCode != UsnJournalReturnCode.USN_JOURNAL_SUCCESS){
                        throw new Win32Exception((int)usnRtnCode);
                    }                    
                }
                else{
                    throw new Win32Exception((int)usnRtnCode);
                }
            }
            else{
                throw new Exception(this.driveInfo.Name + " is not an 'NTFS' volume.");
            }
        }

        #endregion

        #region public methods        
        public UsnJournalReturnCode GetFilesMatchingName(out FileSet[] results, string filter, SearchType search, ComparisonType comparison){
        	results = null ;
            filter = filter.ToLower();
            ConcurrentBag<NtfsWinApi.UsnEntry> files = new ConcurrentBag<NtfsWinApi.UsnEntry>()  ;
            ConcurrentDictionary<ulong, NtfsWinApi.UsnEntry> dirs = new ConcurrentDictionary<ulong, NtfsWinApi.UsnEntry>() ;            
            UsnJournalReturnCode usnRtnCode = UsnJournalReturnCode.VOLUME_NOT_NTFS;

            if (bNtfsVolume){
                if (this.usnJournalRootHandle.ToInt32() != NtfsWinApi.INVALID_HANDLE_VALUE){
                    usnRtnCode = UsnJournalReturnCode.USN_JOURNAL_SUCCESS;

                    NtfsWinApi.USN_JOURNAL_DATA usnState = new NtfsWinApi.USN_JOURNAL_DATA();
                    usnRtnCode = QueryUsnJournal(ref usnState);

                    if (usnRtnCode == UsnJournalReturnCode.USN_JOURNAL_SUCCESS){
                        //
                        // set up MFT_ENUM_DATA structure
                        //
                        NtfsWinApi.MFT_ENUM_DATA med;
                        med.StartFileReferenceNumber = 0;
                        med.LowUsn = 0;
                        med.HighUsn = usnState.NextUsn;
                        Int32 sizeMftEnumData = Marshal.SizeOf(med);
                        IntPtr medBuffer = Marshal.AllocHGlobal(sizeMftEnumData);
                        NtfsWinApi.ZeroMemory(medBuffer, sizeMftEnumData);
                        Marshal.StructureToPtr(med, medBuffer, true);

                        //
                        // set up the data buffer which receives the USN_RECORD data
                        //
                        int pDataSize = sizeof(UInt64) + 10000;
                        IntPtr pData = Marshal.AllocHGlobal(pDataSize);
                        NtfsWinApi.ZeroMemory(pData, pDataSize);
                        uint outBytesReturned = 0;
                        NtfsWinApi.UsnEntry usnEntry = null;

                        
                        CompareInfo compInf = CultureInfo.InvariantCulture.CompareInfo ;
                        CompareOptions compOpts = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace ;
                        //
                        // Gather up volume's directories
                        //
                        while (NtfsWinApi.DeviceIoControl(
                            this.usnJournalRootHandle,
                            NtfsWinApi.FSCTL_ENUM_USN_DATA,
                            medBuffer,
                            sizeMftEnumData,
                            pData,
                            pDataSize,
                            out outBytesReturned,
                            IntPtr.Zero)
                           ){ 
                            IntPtr pUsnRecord = new IntPtr((this.IsX64 ? pData.ToInt64() : pData.ToInt32()) + sizeof(Int64));
                            while (outBytesReturned > 60){
                                usnEntry = new NtfsWinApi.UsnEntry(pUsnRecord);
                                if(
                                	search == SearchType.FILES_FOLDERS ||
                                	(search == SearchType.FILES_ONLY && usnEntry.IsFile) ||
                                	(search == SearchType.FOLDERS_ONLY && usnEntry.IsFolder)
                                ){
                                	switch (comparison) {
                                		case ComparisonType.EXACT_MATCH:
                                			if(compInf.Compare(usnEntry.Name, filter, compOpts) == 0)
                                				files.Add(usnEntry) ;
                                			break ;
                                		case ComparisonType.CONTAINS:
                                			if(compInf.IndexOf(usnEntry.Name, filter, compOpts)  > -1)                             			
                                				files.Add(usnEntry) ;
                                			break ;
                                		case ComparisonType.STARTS_WITH:
                                			if(compInf.IndexOf(usnEntry.Name, filter, compOpts) == 0)
                                				files.Add(usnEntry) ;
                                			break ;
                                		case ComparisonType.ENDS_WITH:
                                			if(usnEntry.Name.Length >= filter.Length){
                                				if(compInf.LastIndexOf(usnEntry.Name, filter, compOpts) + filter.Length == usnEntry.Name.Length){
                                					files.Add(usnEntry) ;
                                				}
                                			}
                                			break ;
                                	}
                                	/*if(exactMatch){
                                		if(compInf.Compare(usnEntry.Name, filter, compOpts) == 0){
                                			files.Add(usnEntry) ;
                                		}
                                	}else{
                                		if(compInf.IndexOf(usnEntry.Name, filter, compOpts)  > -1){                                			
                                			files.Add(usnEntry) ;
                                		}
                                	}*/
                        				
                                }
                                
                                if(usnEntry.IsFolder)
                                	dirs.TryAdd(usnEntry.FileReferenceNumber, usnEntry) ;
                                
                                pUsnRecord = new IntPtr((this.IsX64 ? pUsnRecord.ToInt64() : pUsnRecord.ToInt32()) + usnEntry.RecordLength);
                                outBytesReturned -= usnEntry.RecordLength;
                            }
                            Marshal.WriteInt64(medBuffer, Marshal.ReadInt64(pData, 0));
                        }

                        Marshal.FreeHGlobal(pData);
                        usnRtnCode = ConvertWin32ErrorToUsnError((NtfsWinApi.GetLastErrorEnum)Marshal.GetLastWin32Error());
                        if (usnRtnCode == UsnJournalReturnCode.ERROR_HANDLE_EOF)
                            usnRtnCode = UsnJournalReturnCode.USN_JOURNAL_SUCCESS;
            
			            if (usnRtnCode == UsnJournalReturnCode.USN_JOURNAL_SUCCESS){
				            ConcurrentBag<FileSet> resultsBag = new ConcurrentBag<FileSet>() ;
				            files.AsParallel().WithDegreeOfParallelism(Environment.ProcessorCount).ForAll(
				            	file => {
				            		Stack<string> names = new Stack<string>() ;
				            		NtfsWinApi.UsnEntry current = file ;
				            		do{
				            			names.Push(current.Name) ;
				            		}while(dirs.TryGetValue(current.ParentFileReferenceNumber, out current)) ;            		
				            		resultsBag.Add(new FileSet(file.Name, (this.driveInfo.Name + string.Join("\\", names)))) ;
				            	}) ;
				            
				            results = resultsBag.ToArray() ;
			            }
                    }
                }
                else{
                    usnRtnCode = UsnJournalReturnCode.INVALID_HANDLE_VALUE;
                }
            }
            
            return usnRtnCode;
        }

        /// <summary>
        /// GetUsnJournalState() gets the current state of the USN Journal if it is active.
        /// </summary>
        /// <param name="usnJournalState">
        /// Reference to usn journal data object filled with the current USN Journal state.
        /// </param>
        /// <param name="elapsedTime">The elapsed time for the GetUsnJournalState() function call.</param>
        /// <returns>
        /// USN_JOURNAL_SUCCESS                 GetUsnJournalState() function succeeded. 
        /// VOLUME_NOT_NTFS                     volume is not an NTFS volume.
        /// INVALID_HANDLE_VALUE                NtfsUsnJournal object failed initialization.
        /// USN_JOURNAL_NOT_ACTIVE              usn journal is not active on volume.
        /// ERROR_ACCESS_DENIED                 accessing the usn journal requires admin rights, see remarks.
        /// ERROR_INVALID_FUNCTION              error generated by DeviceIoControl() call.
        /// ERROR_FILE_NOT_FOUND                error generated by DeviceIoControl() call.
        /// ERROR_PATH_NOT_FOUND                error generated by DeviceIoControl() call.
        /// ERROR_TOO_MANY_OPEN_FILES           error generated by DeviceIoControl() call.
        /// ERROR_INVALID_HANDLE                error generated by DeviceIoControl() call.
        /// ERROR_INVALID_DATA                  error generated by DeviceIoControl() call.
        /// ERROR_NOT_SUPPORTED                 error generated by DeviceIoControl() call.
        /// ERROR_INVALID_PARAMETER             error generated by DeviceIoControl() call.
        /// ERROR_JOURNAL_DELETE_IN_PROGRESS    usn journal delete is in progress.
        /// ERROR_INVALID_USER_BUFFER           error generated by DeviceIoControl() call.
        /// USN_JOURNAL_ERROR                   unspecified usn journal error.
        /// </returns>
        /// <remarks>
        /// If function returns ERROR_ACCESS_DENIED you need to run application as an Administrator.
        /// </remarks>
        public UsnJournalReturnCode GetUsnJournalState(ref NtfsWinApi.USN_JOURNAL_DATA usnJournalState){
            UsnJournalReturnCode usnRtnCode = UsnJournalReturnCode.VOLUME_NOT_NTFS;

            if (bNtfsVolume){
                if (this.usnJournalRootHandle.ToInt32() != NtfsWinApi.INVALID_HANDLE_VALUE){
                    usnRtnCode = QueryUsnJournal(ref usnJournalState);
                }else{
                    usnRtnCode = UsnJournalReturnCode.INVALID_HANDLE_VALUE;
                }
            }

            return usnRtnCode;
        }

        /// <summary>
        /// tests to see if the USN Journal is active on the volume.
        /// </summary>
        /// <returns>true if USN Journal is active
        /// false if no USN Journal on volume</returns>
        public bool IsUsnJournalActive(){
            bool bRtnCode = false;

            if (bNtfsVolume){
                if (this.usnJournalRootHandle.ToInt32() != NtfsWinApi.INVALID_HANDLE_VALUE){
                    NtfsWinApi.USN_JOURNAL_DATA usnJournalCurrentState = new NtfsWinApi.USN_JOURNAL_DATA();
                    UsnJournalReturnCode usnError = QueryUsnJournal(ref usnJournalCurrentState);
                    if (usnError == UsnJournalReturnCode.USN_JOURNAL_SUCCESS){
                        bRtnCode = true;
                    }
                }
            }
            return bRtnCode;
        }

        /// <summary>
        /// tests to see if there is a USN Journal on this volume and if there is 
        /// determines whether any journal entries have been lost.
        /// </summary>
        /// <returns>true if the USN Journal is active and if the JournalId's are the same 
        /// and if all the usn journal entries expected by the previous state are available 
        /// from the current state.
        /// false if not</returns>
        public bool IsUsnJournalValid(NtfsWinApi.USN_JOURNAL_DATA usnJournalPreviousState){
            bool bRtnCode = false;

            if (bNtfsVolume){
                if (this.usnJournalRootHandle.ToInt32() != NtfsWinApi.INVALID_HANDLE_VALUE){
                    NtfsWinApi.USN_JOURNAL_DATA usnJournalState = new NtfsWinApi.USN_JOURNAL_DATA();
                    UsnJournalReturnCode usnError = QueryUsnJournal(ref usnJournalState);

                    if (usnError == UsnJournalReturnCode.USN_JOURNAL_SUCCESS){
                        if (usnJournalPreviousState.UsnJournalID == usnJournalState.UsnJournalID){
                            if (usnJournalPreviousState.NextUsn >= usnJournalState.NextUsn){
                                bRtnCode = true;
                            }
                        }
                    }
                }
            }
            return bRtnCode;
        }

        #endregion

        #region private member functions 
        
        /// <summary>
        /// Converts a Win32 Error to a UsnJournalReturnCode
        /// </summary>
        /// <param name="Win32LastError">The 'last' Win32 error.</param>
        /// <returns>
        /// INVALID_HANDLE_VALUE                error generated by Win32 Api calls.
        /// USN_JOURNAL_SUCCESS                 usn journal function succeeded. 
        /// ERROR_INVALID_FUNCTION              error generated by Win32 Api calls.
        /// ERROR_FILE_NOT_FOUND                error generated by Win32 Api calls.
        /// ERROR_PATH_NOT_FOUND                error generated by Win32 Api calls.
        /// ERROR_TOO_MANY_OPEN_FILES           error generated by Win32 Api calls.
        /// ERROR_ACCESS_DENIED                 accessing the usn journal requires admin rights.
        /// ERROR_INVALID_HANDLE                error generated by Win32 Api calls.
        /// ERROR_INVALID_DATA                  error generated by Win32 Api calls.
        /// ERROR_HANDLE_EOF                    error generated by Win32 Api calls.
        /// ERROR_NOT_SUPPORTED                 error generated by Win32 Api calls.
        /// ERROR_INVALID_PARAMETER             error generated by Win32 Api calls.
        /// ERROR_JOURNAL_DELETE_IN_PROGRESS    usn journal delete is in progress.
        /// ERROR_JOURNAL_ENTRY_DELETED         usn journal entry lost, no longer available.
        /// ERROR_INVALID_USER_BUFFER           error generated by Win32 Api calls.
        /// USN_JOURNAL_INVALID                 usn journal is invalid, id's don't match or required entries lost.
        /// USN_JOURNAL_NOT_ACTIVE              usn journal is not active on volume.
        /// VOLUME_NOT_NTFS                     volume is not an NTFS volume.
        /// INVALID_FILE_REFERENCE_NUMBER       bad file reference number - see remarks.
        /// USN_JOURNAL_ERROR                   unspecified usn journal error.
        /// </returns>
        private UsnJournalReturnCode ConvertWin32ErrorToUsnError(NtfsWinApi.GetLastErrorEnum Win32LastError){
            UsnJournalReturnCode usnRtnCode;

            switch (Win32LastError){
                case NtfsWinApi.GetLastErrorEnum.ERROR_JOURNAL_NOT_ACTIVE:
                    usnRtnCode = UsnJournalReturnCode.USN_JOURNAL_NOT_ACTIVE;
                    break;
                case NtfsWinApi.GetLastErrorEnum.ERROR_SUCCESS:
                    usnRtnCode = UsnJournalReturnCode.USN_JOURNAL_SUCCESS;
                    break;
                case NtfsWinApi.GetLastErrorEnum.ERROR_HANDLE_EOF:
                    usnRtnCode = UsnJournalReturnCode.ERROR_HANDLE_EOF;
                    break;
                default:
                    usnRtnCode = UsnJournalReturnCode.USN_JOURNAL_ERROR;
                    break;
            }

            return usnRtnCode;
        }

        /// <summary>
        /// Gets a Volume Serial Number for the volume represented by driveInfo.
        /// </summary>
        /// <param name="driveInfo">DriveInfo object representing the volume in question.</param>
        /// <param name="volumeSerialNumber">out parameter to hold the volume serial number.</param>
        /// <returns></returns>
        private UsnJournalReturnCode GetVolumeSerialNumber(DriveInfo driveInfo, out uint volumeSerialNumber){
            volumeSerialNumber = 0;
            UsnJournalReturnCode usnRtnCode = UsnJournalReturnCode.USN_JOURNAL_SUCCESS;
            string pathRoot = "\\\\.\\" + driveInfo.Name;

            IntPtr hRoot = NtfsWinApi.CreateFile(pathRoot,
                0,
                NtfsWinApi.FILE_SHARE_READ | NtfsWinApi.FILE_SHARE_WRITE,
                IntPtr.Zero,
                NtfsWinApi.OPEN_EXISTING,
                NtfsWinApi.FILE_FLAG_BACKUP_SEMANTICS,
                IntPtr.Zero);

            if (hRoot.ToInt32() != NtfsWinApi.INVALID_HANDLE_VALUE){
                NtfsWinApi.BY_HANDLE_FILE_INFORMATION fi = new NtfsWinApi.BY_HANDLE_FILE_INFORMATION();
                bool bRtn = NtfsWinApi.GetFileInformationByHandle(hRoot, out fi);

                if (bRtn){
                    UInt64 fileIndexHigh = (UInt64)fi.FileIndexHigh;
                    UInt64 indexRoot = (fileIndexHigh << 32) | fi.FileIndexLow;
                    volumeSerialNumber = fi.VolumeSerialNumber;
                }else{
                    usnRtnCode = (UsnJournalReturnCode)Marshal.GetLastWin32Error();
                }

                NtfsWinApi.CloseHandle(hRoot);
            }else{
                usnRtnCode = (UsnJournalReturnCode)Marshal.GetLastWin32Error();
            }
            return usnRtnCode;
        }

        private UsnJournalReturnCode GetRootHandle(out IntPtr rootHandle){
            //
            // private functions don't need to check for an NTFS volume or
            // a valid this.usnJournalRootHandle handle
            //
            UsnJournalReturnCode usnRtnCode = UsnJournalReturnCode.USN_JOURNAL_SUCCESS;
            rootHandle = IntPtr.Zero;
            string vol = "\\\\.\\" + this.driveInfo.Name.TrimEnd('\\');

            rootHandle = NtfsWinApi.CreateFile(
            	vol,
                NtfsWinApi.GENERIC_READ | NtfsWinApi.GENERIC_WRITE,
                NtfsWinApi.FILE_SHARE_READ | NtfsWinApi.FILE_SHARE_WRITE,
                IntPtr.Zero,
                NtfsWinApi.OPEN_EXISTING,
                0,
                IntPtr.Zero
               );

            if (rootHandle.ToInt32() == NtfsWinApi.INVALID_HANDLE_VALUE){
                usnRtnCode = (UsnJournalReturnCode)Marshal.GetLastWin32Error();
            }

            return usnRtnCode;
        }

        /// <summary>
        /// This function queries the usn journal on the volume. 
        /// </summary>
        /// <param name="usnJournalState">the USN_JOURNAL_DATA object that is associated with this volume</param>
        /// <returns></returns>
        private UsnJournalReturnCode QueryUsnJournal(ref NtfsWinApi.USN_JOURNAL_DATA usnJournalState){
            //
            // private functions don't need to check for an NTFS volume or
            // a valid this.usnJournalRootHandle handle
            //
            UsnJournalReturnCode usnReturnCode = UsnJournalReturnCode.USN_JOURNAL_SUCCESS;
            int sizeUsnJournalState = Marshal.SizeOf(usnJournalState);
            UInt32 cb;

            bool fOk = NtfsWinApi.DeviceIoControl(
                this.usnJournalRootHandle,
                NtfsWinApi.FSCTL_QUERY_USN_JOURNAL,
                IntPtr.Zero,
                0,
                out usnJournalState,
                sizeUsnJournalState,
                out cb,
                IntPtr.Zero
               );

            if (!fOk){
                int lastWin32Error = Marshal.GetLastWin32Error();
                usnReturnCode = ConvertWin32ErrorToUsnError((NtfsWinApi.GetLastErrorEnum)Marshal.GetLastWin32Error());
            }

            return usnReturnCode;
        }

        #endregion

        #region IDisposable Members

        public void Dispose(){
            NtfsWinApi.CloseHandle(this.usnJournalRootHandle);
        }

        #endregion
    }
}

