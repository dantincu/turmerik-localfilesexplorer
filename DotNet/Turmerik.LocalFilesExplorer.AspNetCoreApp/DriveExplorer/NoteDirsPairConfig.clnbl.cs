using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public interface INoteDirsPairConfig
    {
        int? FileNameMaxLength { get; }
        bool? SerializeToJson { get; }
        bool? AllowGetRequestsToPersistChanges { get; }
        string? TrmrkGuidInputName { get; }
        NoteDirsPairConfig.IArgOptionsAggT GetArgOpts();
        NoteDirsPairConfig.IDirNamesT GetDirNames();
        NoteDirsPairConfig.IDirNameIdxesT GetNoteDirNameIdxes();
        NoteDirsPairConfig.IDirNameIdxesT GetNoteInternalDirNameIdxes();
        NoteDirsPairConfig.IFileNamesT GetFileNames();
        NoteDirsPairConfig.IFileContentsT GetFileContents();
    }

    public static class NoteDirsPairConfig
    {
        public interface IArgOptionT
        {
            CmdCommand? Command { get; }
            string FullArg { get; }
            string ShortArg { get; }
            string Description { get; }
        }

        public interface IArgOptionsAggT
        {
            IArgOptionT GetHelp();
            IArgOptionT GetSrcNote();
            IArgOptionT GetSrcDirIdnf();
            IArgOptionT GetSrcNoteIdx();
            IArgOptionT GetDestnNote();
            IArgOptionT GetDestnDirIdnf();
            IArgOptionT GetDestnNoteIdx();
            IArgOptionT GetNotesOrder();
            IArgOptionT GetNoteIdxesOrder();
            IArgOptionT GetIsPinned();
            IArgOptionT GetSortIdx();
            IArgOptionT GetNoteIdx();
            IArgOptionT GetOpenMdFile();
            IArgOptionT GetReorderNotes();
            IArgOptionT GetCreateNoteFilesDirsPair();
            IArgOptionT GetCreateNoteInternalDirsPair();

            ClnblDictionary<CmdCommand, IArgOptionT, NoteDirsPairConfigImmtbl.ArgOptionT, NoteDirsPairConfigMtbl.ArgOptionT> GetCommandsMap();
        }

        public interface IDirNamesT
        {
            string NoteBook { get; }
            string NoteFiles { get; }
            string NoteInternals { get; }

            IDirNamePfxesT GetNoteInternalsPfxes();
            IDirNamePfxesT GetNoteItemsPfxes();
        }

        public interface IDirNamePfxesT
        {
            string MainPfx { get; }
            string AltPfx { get; }
            string JoinStr { get; }
            bool? UseAltPfx { get; }
        }

        public interface IDirNameIdxesT
        {
            int? MinIdx { get; }
            int? MaxIdx { get; }
            bool? IncIdx { get; }
            bool? FillGapsByDefault { get; }
            string? IdxFmt { get; }
        }

        public interface IFileNamesT
        {
            string NoteBookJsonFileName { get; }
            string NoteItemJsonFileName { get; }
            string NoteItemMdFileName { get; }
            bool? PrependTitleToNoteMdFileName { get; }
            string KeepFileName { get; }
        }

        public interface IFileContentsT
        {
            string KeepFileContentsTemplate { get; }
            string NoteFileContentsTemplate { get; }
            string NoteFileContentSectionTemplate { get; }
            bool? RequireTrmrkGuidInNoteJsonFile { get; }
            bool? RequireTrmrkGuidInNoteMdFile { get; }
        }

        public static NoteDirsPairConfigImmtbl ToImmtbl(
            this INoteDirsPairConfig src) => new NoteDirsPairConfigImmtbl(src);

        public static NoteDirsPairConfigImmtbl.ArgOptionT ToImmtbl(
            this IArgOptionT src) => new NoteDirsPairConfigImmtbl.ArgOptionT(src);

        public static NoteDirsPairConfigImmtbl.ArgOptionsAggT ToImmtbl(
            this IArgOptionsAggT src) => new NoteDirsPairConfigImmtbl.ArgOptionsAggT(src);

        public static NoteDirsPairConfigImmtbl.DirNamesT ToImmtbl(
            this IDirNamesT src) => new NoteDirsPairConfigImmtbl.DirNamesT(src);

        public static NoteDirsPairConfigImmtbl.DirNamePfxesT ToImmtbl(
            this IDirNamePfxesT src) => new NoteDirsPairConfigImmtbl.DirNamePfxesT(src);

        public static NoteDirsPairConfigImmtbl.DirNameIdxesT ToImmtbl(
            this IDirNameIdxesT src) => new NoteDirsPairConfigImmtbl.DirNameIdxesT(src);

        public static NoteDirsPairConfigImmtbl.FileNamesT ToImmtbl(
            this IFileNamesT src) => new NoteDirsPairConfigImmtbl.FileNamesT(src);

        public static NoteDirsPairConfigImmtbl.FileContentsT ToImmtbl(
            this IFileContentsT src) => new NoteDirsPairConfigImmtbl.FileContentsT(src);

        public static NoteDirsPairConfigMtbl ToMtbl(
            this INoteDirsPairConfig src) => new NoteDirsPairConfigMtbl(src);

        public static NoteDirsPairConfigMtbl.ArgOptionT ToMtbl(
            this IArgOptionT src) => new NoteDirsPairConfigMtbl.ArgOptionT(src);

        public static NoteDirsPairConfigMtbl.ArgOptionsAggT ToMtbl(
            this IArgOptionsAggT src) => new NoteDirsPairConfigMtbl.ArgOptionsAggT(src);

        public static NoteDirsPairConfigMtbl.DirNamesT ToMtbl(
            this IDirNamesT src) => new NoteDirsPairConfigMtbl.DirNamesT(src);

        public static NoteDirsPairConfigMtbl.DirNamePfxesT ToMtbl(
            this IDirNamePfxesT src) => new NoteDirsPairConfigMtbl.DirNamePfxesT(src);

        public static NoteDirsPairConfigMtbl.DirNameIdxesT ToMtbl(
            this IDirNameIdxesT src) => new NoteDirsPairConfigMtbl.DirNameIdxesT(src);

        public static NoteDirsPairConfigMtbl.FileNamesT ToMtbl(
            this IFileNamesT src) => new NoteDirsPairConfigMtbl.FileNamesT(src);

        public static NoteDirsPairConfigMtbl.FileContentsT ToMtbl(
            this IFileContentsT src) => new NoteDirsPairConfigMtbl.FileContentsT(src);

        public static bool Matches(
            this IArgOptionT option,
            string rawArg) => option.ShortArg == rawArg || option.FullArg == rawArg;

        public static string[] ToStrArr(
            this IArgOptionT option) => new string[] { option.ShortArg, option.FullArg };
    }
}
