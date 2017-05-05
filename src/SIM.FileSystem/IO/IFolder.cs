﻿using System.Collections.Generic;

namespace SIM.IO
{
  using System;

  public interface IFolder : IFileSystemEntry, IEquatable<IFolder>
  {
    void Create();

    bool Exists { get; }

    IReadOnlyList<IFileSystemEntry> GetChildren();

    IReadOnlyList<IFile> GetFiles();

    IReadOnlyList<IFolder> GetFolders();

    IFolder MoveTo(IFolder parent);
  }
}