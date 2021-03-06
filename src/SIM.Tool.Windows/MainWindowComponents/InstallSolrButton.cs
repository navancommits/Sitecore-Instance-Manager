﻿using System;
using JetBrains.Annotations;
using SIM.Instances;
using SIM.Tool.Base.Pipelines;
using SIM.Tool.Base.Plugins;
using SIM.Tool.Base.Wizards;
using System.Windows;

namespace SIM.Tool.Windows.MainWindowComponents
{
  public class InstallSolrButton : IMainWindowButton
  {
    public event EventHandler<InstallWizardArgs> InstallationCompleted;

    public bool IsEnabled([NotNull] Window mainWindow, [CanBeNull] Instance instance)
    {
      return true;
    }

    public bool IsVisible([NotNull] Window mainWindow, [CanBeNull] Instance instance)
    {
      return true;
    }

    public void OnClick([NotNull] Window mainWindow, [CanBeNull] Instance instance)
    {
      this.InstallSolr(mainWindow);
    }

    public void InstallSolr(Window window, bool? isAsync = null)
    {
      WizardPipelineManager.Start("installSolr", window, null, isAsync, (args) =>
      {
        if (args == null)
        {
          return;
        }

        var install = (InstallWizardArgs)args;
        if (install.HasInstallationBeenCompleted)
        {
          MainWindowHelper.SoftlyRefreshInstances();
        }

        // Raise the event to refresh the list of Solr servers after installing the new one
        InstallationCompleted?.Invoke(this, install);
      }, () => new Install9WizardArgs());
    }
  }
}
