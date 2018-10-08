﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    internal class StartEditingFileAttachmentMessage
    {
        public FileAttachmentSearchResultModel FileAttachmentSearchResult { get; }

        public StartEditingFileAttachmentMessage(FileAttachmentSearchResultModel fileAttachmentSearchResult)
        {
            FileAttachmentSearchResult = fileAttachmentSearchResult;
        }
    }
}
