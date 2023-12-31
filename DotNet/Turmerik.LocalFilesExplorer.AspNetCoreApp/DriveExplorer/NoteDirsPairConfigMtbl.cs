using Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;
using static Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer.NoteDirsPairConfig;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public class NoteDirsPairConfigMtbl : INoteDirsPairConfig
    {
        public NoteDirsPairConfigMtbl(
            INoteDirsPairConfig src)
        {
            FileNameMaxLength = src.FileNameMaxLength;
            SerializeToJson = src.SerializeToJson;
            AllowGetRequestsToPersistChanges = src.AllowGetRequestsToPersistChanges;
            TrmrkGuidInputName = src.TrmrkGuidInputName;

            ArgOpts = src.GetArgOpts()?.ToMtbl();
            DirNames = src.GetDirNames()?.ToMtbl();
            NoteDirNameIdxes = src.GetNoteDirNameIdxes()?.ToMtbl();
            NoteInternalDirNameIdxes = src.GetNoteInternalDirNameIdxes()?.ToMtbl();
            FileNames = src.GetFileNames()?.ToMtbl();
            FileContents = src.GetFileContents()?.ToMtbl();
        }

        public NoteDirsPairConfigMtbl()
        {
        }

        public int? FileNameMaxLength { get; set; }
        public bool? SerializeToJson { get; set; }
        public bool? AllowGetRequestsToPersistChanges { get; set; }
        public string? TrmrkGuidInputName { get; set; }
        public ArgOptionsAggT ArgOpts { get; set; }
        public DirNamesT DirNames { get; set; }
        public DirNameIdxesT NoteDirNameIdxes { get; set; }
        public DirNameIdxesT NoteInternalDirNameIdxes { get; set; }
        public FileNamesT FileNames { get; set; }
        public FileContentsT FileContents { get; set; }

        public IArgOptionsAggT GetArgOpts() => ArgOpts;
        public IDirNamesT GetDirNames() => DirNames;
        public IDirNameIdxesT GetNoteDirNameIdxes() => NoteDirNameIdxes;
        public IDirNameIdxesT GetNoteInternalDirNameIdxes() => NoteInternalDirNameIdxes;
        public IFileNamesT GetFileNames() => FileNames;
        public IFileContentsT GetFileContents() => FileContents;

        public class ArgOptionT : IArgOptionT
        {
            public ArgOptionT()
            {
            }

            public ArgOptionT(IArgOptionT src)
            {
                Command = src.Command;
                FullArg = src.FullArg;
                ShortArg = src.ShortArg;
                Description = src.Description;
            }

            public CmdCommand? Command { get; set; }
            public string FullArg { get; set; }
            public string ShortArg { get; set; }
            public string Description { get; set; }
        }

        public class ArgOptionsAggT : IArgOptionsAggT
        {
            private ClnblDictionary<CmdCommand, IArgOptionT, NoteDirsPairConfigImmtbl.ArgOptionT, ArgOptionT> commandsClnblMap;
            private Dictionary<CmdCommand, ArgOptionT> commandsMap;

            public ArgOptionsAggT()
            {
            }

            public ArgOptionsAggT(IArgOptionsAggT src)
            {
                Help = src.GetHelp()?.ToMtbl();
                SrcNote = src.GetSrcNote()?.ToMtbl();
                SrcDirIdnf = src.GetSrcDirIdnf()?.ToMtbl();
                SrcNoteIdx = src.GetSrcNoteIdx()?.ToMtbl();
                DestnNote = src.GetDestnNote()?.ToMtbl();
                DestnDirIdnf = src.GetDestnDirIdnf()?.ToMtbl();
                DestnNoteIdx = src.GetDestnNoteIdx()?.ToMtbl();
                IsPinned = src.GetIsPinned()?.ToMtbl();
                SortIdx = src.GetSortIdx()?.ToMtbl();
                NoteIdx = src.GetNoteIdx()?.ToMtbl();
                OpenMdFile = src.GetOpenMdFile()?.ToMtbl();
                ReorderNotes = src.GetReorderNotes()?.ToMtbl();
                CreateNoteFilesDirsPair = src.GetCreateNoteFilesDirsPair()?.ToMtbl();
                CreateNoteInternalDirsPair = src.GetCreateNoteInternalDirsPair()?.ToMtbl();

                CommandsMap = src.GetCommandsMap()?.AsMtblDictnr();
            }

            public ArgOptionT Help { get; set; }
            public ArgOptionT SrcNote { get; set; }
            public ArgOptionT SrcDirIdnf { get; set; }
            public ArgOptionT SrcNoteIdx { get; set; }
            public ArgOptionT DestnNote { get; set; }
            public ArgOptionT DestnDirIdnf { get; set; }
            public ArgOptionT DestnNoteIdx { get; set; }
            public ArgOptionT NotesOrder { get; set; }
            public ArgOptionT NoteIdxesOrder { get; set; }
            public ArgOptionT IsPinned { get; set; }
            public ArgOptionT SortIdx { get; set; }
            public ArgOptionT NoteIdx { get; set; }
            public ArgOptionT OpenMdFile { get; set; }
            public ArgOptionT ReorderNotes { get; set; }
            public ArgOptionT CreateNoteFilesDirsPair { get; set; }
            public ArgOptionT CreateNoteInternalDirsPair { get; set; }

            public Dictionary<CmdCommand, ArgOptionT> CommandsMap
            {
                get => commandsMap;

                set
                {
                    commandsMap = value;
                    commandsClnblMap = new ClnblDictionary<CmdCommand, IArgOptionT, NoteDirsPairConfigImmtbl.ArgOptionT, ArgOptionT>(value);
                }
            }

            public IArgOptionT GetHelp() => Help;
            public IArgOptionT GetSrcNote() => SrcNote;
            public IArgOptionT GetSrcDirIdnf() => SrcDirIdnf;
            public IArgOptionT GetSrcNoteIdx() => SrcNoteIdx;
            public IArgOptionT GetDestnNote() => DestnNote;
            public IArgOptionT GetDestnDirIdnf() => DestnDirIdnf;
            public IArgOptionT GetDestnNoteIdx() => DestnNoteIdx;
            public IArgOptionT GetNotesOrder() => NotesOrder;
            public IArgOptionT GetNoteIdxesOrder() => NoteIdxesOrder;
            public IArgOptionT GetIsPinned() => IsPinned;
            public IArgOptionT GetSortIdx() => SortIdx;
            public IArgOptionT GetNoteIdx() => NoteIdx;
            public IArgOptionT GetOpenMdFile() => OpenMdFile;
            public IArgOptionT GetReorderNotes() => ReorderNotes;
            public IArgOptionT GetCreateNoteFilesDirsPair() => CreateNoteFilesDirsPair;
            public IArgOptionT GetCreateNoteInternalDirsPair() => CreateNoteInternalDirsPair;

            public ClnblDictionary<CmdCommand, IArgOptionT, NoteDirsPairConfigImmtbl.ArgOptionT, ArgOptionT> GetCommandsMap() => commandsClnblMap;
        }

        public class DirNamesT : IDirNamesT
        {
            public DirNamesT()
            {
            }

            public DirNamesT(IDirNamesT src)
            {
                NoteBook = src.NoteBook;
                NoteFiles = src.NoteFiles;
                NoteInternals = src.NoteInternals;
                NoteInternalsPfxes = src.GetNoteInternalsPfxes()?.ToMtbl();
                NoteItemsPfxes = src.GetNoteItemsPfxes()?.ToMtbl();
            }

            public string NoteBook { get; set; }
            public string NoteFiles { get; set; }
            public string NoteInternals { get; set; }

            public DirNamePfxesT NoteInternalsPfxes { get; set; }
            public DirNamePfxesT NoteItemsPfxes { get; set; }

            public IDirNamePfxesT GetNoteInternalsPfxes() => NoteInternalsPfxes;
            public IDirNamePfxesT GetNoteItemsPfxes() => NoteItemsPfxes;
        }

        public class DirNamePfxesT : IDirNamePfxesT
        {
            public DirNamePfxesT()
            {
            }

            public DirNamePfxesT(IDirNamePfxesT src)
            {
                MainPfx = src.MainPfx;
                AltPfx = src.AltPfx;
                JoinStr = src.JoinStr;
                UseAltPfx = src.UseAltPfx;
            }

            public string MainPfx { get; set; }
            public string AltPfx { get; set; }
            public string JoinStr { get; set; }
            public bool? UseAltPfx { get; set; }
        }

        public class DirNameIdxesT : IDirNameIdxesT
        {
            public DirNameIdxesT()
            {
            }

            public DirNameIdxesT(IDirNameIdxesT src)
            {
                MinIdx = src.MinIdx;
                MaxIdx = src.MaxIdx;
                IncIdx = src.IncIdx;
                FillGapsByDefault = src.FillGapsByDefault;
                IdxFmt = src.IdxFmt;
            }

            public int? MinIdx { get; set; }
            public int? MaxIdx { get; set; }
            public bool? IncIdx { get; set; }
            public bool? FillGapsByDefault { get; set; }
            public string? IdxFmt { get; set; }
        }

        public class FileNamesT : IFileNamesT
        {
            public FileNamesT()
            {
            }

            public FileNamesT(IFileNamesT src)
            {
                NoteBookJsonFileName = src.NoteBookJsonFileName;
                NoteItemJsonFileName = src.NoteItemJsonFileName;
                NoteItemMdFileName = src.NoteItemMdFileName;
                PrependTitleToNoteMdFileName = src.PrependTitleToNoteMdFileName;
                KeepFileName = src.KeepFileName;
            }

            public string NoteBookJsonFileName { get; set; }
            public string NoteItemJsonFileName { get; set; }
            public string NoteItemMdFileName { get; set; }
            public bool? PrependTitleToNoteMdFileName { get; set; }
            public string KeepFileName { get; set; }
        }

        public class FileContentsT : IFileContentsT
        {
            public FileContentsT()
            {
            }

            public FileContentsT(IFileContentsT src)
            {
                KeepFileContentsTemplate = src.KeepFileContentsTemplate;
                NoteFileContentsTemplate = src.NoteFileContentsTemplate;
                NoteFileContentSectionTemplate = src.NoteFileContentSectionTemplate;
                RequireTrmrkGuidInNoteJsonFile = src.RequireTrmrkGuidInNoteJsonFile;
                RequireTrmrkGuidInNoteMdFile = src.RequireTrmrkGuidInNoteMdFile;
            }

            public string KeepFileContentsTemplate { get; set; }
            public string NoteFileContentsTemplate { get; set; }
            public string NoteFileContentSectionTemplate { get; set; }
            public bool? RequireTrmrkGuidInNoteJsonFile { get; set; }
            public bool? RequireTrmrkGuidInNoteMdFile { get; set; }
        }
    }
}
