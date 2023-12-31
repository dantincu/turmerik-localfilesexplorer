using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.DriveExplorer
{
    public enum CmdCommand
    {
        Help = 1,
        ListNotes,
        CreateNoteBook,
        CreateNoteBookInternal,
        CreateNote,
        CreateNoteInternal,
        CopyNotes,
        DeleteNotes,
        MoveNotes,
        RenameNote,
        UpdateNote,
        ReorderNotes,
        NormalizeNote,
        NormalizeNoteIdxes,
        NormalizeNotesHcy,
    }
}
