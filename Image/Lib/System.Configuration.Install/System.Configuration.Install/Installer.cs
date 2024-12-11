using System.Collections;
using System.ComponentModel;
using System.Text;

namespace System.Configuration.Install
{
    /// <summary>Provides the foundation for custom installations.</summary>
    [DefaultEvent("AfterInstall")]
    public class Installer : Component
    {
        private InstallerCollection _installers;

        internal Installer parent;

        private InstallEventHandler _afterCommitHandler;

        private InstallEventHandler _afterInstallHandler;

        private InstallEventHandler _afterRollbackHandler;

        private InstallEventHandler _afterUninstallHandler;

        private InstallEventHandler _beforeCommitHandler;

        private InstallEventHandler _beforeInstallHandler;

        private InstallEventHandler _beforeRollbackHandler;

        private InstallEventHandler _beforeUninstallHandler;

        private const string WrappedExceptionSource = "WrappedExceptionSource";

        /// <summary>Gets or sets information about the current installation.</summary>
        /// <returns>An <see cref="T:System.Configuration.Install.InstallContext" /> that contains information about the current installation.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public InstallContext Context { get; set; }

        /// <summary>Gets the help text for all the installers in the installer collection.</summary>
        /// <returns>The help text for all the installers in the installer collection, including the description of what the installer does and the command line options for the installation executable, such as the InstallUtil.exe utility, that can be passed to and understood by this installer.</returns>
        /// <exception cref="T:System.NullReferenceException">One of the installers in the installer collection specifies a null reference instead of help text. A likely cause for this exception is that a field to contain the help text is defined but not initialized.</exception>
        [Description("Desc_Installer_HelpText")]
        public virtual string HelpText
        {
            get
            {
                var stringBuilder = new StringBuilder();
                for (var i = 0; i < Installers.Count; i++)
                {
                    var helpText = Installers[i].HelpText;
                    if (helpText.Length > 0)
                    {
                        stringBuilder.Append("\r\n");
                        stringBuilder.Append(helpText);
                    }
                }
                return stringBuilder.ToString();
            }
        }

        /// <summary>Gets the collection of installers that this installer contains.</summary>
        /// <returns>An <see cref="T:System.Configuration.Install.InstallerCollection" /> containing the collection of installers associated with this installer.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public InstallerCollection Installers => _installers ?? (_installers = new InstallerCollection(this));

        /// <summary>Gets or sets the installer containing the collection that this installer belongs to.</summary>
        /// <returns>An <see cref="T:System.Configuration.Install.Installer" /> containing the collection that this instance belongs to, or null if this instance does not belong to a collection.</returns>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(InstallerParentConverter))]
        [Description("Desc_Installer_Parent")]
        public Installer Parent
        {
            get => parent;
            set
            {
                if (value == this)
                {
                    throw new InvalidOperationException("安装程序的父级不能等于它自己");
                }
                if (value != parent)
                {
                    if (value != null && InstallerTreeContains(value))
                    {
                        throw new InvalidOperationException("您不能将安装程序的父级设置为创建循环依赖关系的安装程序");
                    }
                    if (parent != null)
                    {
                        var num = parent.Installers.IndexOf(this);
                        if (num != -1)
                        {
                            parent.Installers.RemoveAt(num);
                        }
                    }
                    parent = value;
                    if (parent != null && !parent.Installers.Contains(this)) parent.Installers.Add(this);
                }
            }
        }

        /// <summary>Occurs after all the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property have committed their installations.</summary>
        public event InstallEventHandler Committed
        {
            add => _afterCommitHandler = (InstallEventHandler)Delegate.Combine(_afterCommitHandler, value);
            remove => _afterCommitHandler = (InstallEventHandler)Delegate.Remove(_afterCommitHandler, value);
        }

        /// <summary>Occurs after the <see cref="M:System.Configuration.Install.Installer.Install(System.Collections.IDictionary)" /> methods of all the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property have run.</summary>
        public event InstallEventHandler AfterInstall
        {
            add => _afterInstallHandler = (InstallEventHandler)Delegate.Combine(_afterInstallHandler, value);
            remove => _afterInstallHandler = (InstallEventHandler)Delegate.Remove(_afterInstallHandler, value);
        }

        /// <summary>Occurs after the installations of all the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property are rolled back.</summary>
        public event InstallEventHandler AfterRollback
        {
            add => _afterRollbackHandler = (InstallEventHandler)Delegate.Combine(_afterRollbackHandler, value);
            remove => _afterRollbackHandler = (InstallEventHandler)Delegate.Remove(_afterRollbackHandler, value);
        }

        /// <summary>Occurs after all the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property perform their uninstallation operations.</summary>
        public event InstallEventHandler AfterUninstall
        {
            add => _afterUninstallHandler = (InstallEventHandler)Delegate.Combine(_afterUninstallHandler, value);
            remove => _afterUninstallHandler = (InstallEventHandler)Delegate.Remove(_afterUninstallHandler, value);
        }

        /// <summary>Occurs before the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property committ their installations.</summary>
        public event InstallEventHandler Committing
        {
            add => _beforeCommitHandler = (InstallEventHandler)Delegate.Combine(_beforeCommitHandler, value);
            remove => _beforeCommitHandler = (InstallEventHandler)Delegate.Remove(_beforeCommitHandler, value);
        }

        /// <summary>Occurs before the <see cref="M:System.Configuration.Install.Installer.Install(System.Collections.IDictionary)" /> method of each installer in the installer collection has run.</summary>
        public event InstallEventHandler BeforeInstall
        {
            add => _beforeInstallHandler = (InstallEventHandler)Delegate.Combine(_beforeInstallHandler, value);
            remove => _beforeInstallHandler = (InstallEventHandler)Delegate.Remove(_beforeInstallHandler, value);
        }

        /// <summary>Occurs before the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property are rolled back.</summary>
        public event InstallEventHandler BeforeRollback
        {
            add => _beforeRollbackHandler = (InstallEventHandler)Delegate.Combine(_beforeRollbackHandler, value);
            remove => _beforeRollbackHandler = (InstallEventHandler)Delegate.Remove(_beforeRollbackHandler, value);
        }

        /// <summary>Occurs before the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property perform their uninstall operations.</summary>
        public event InstallEventHandler BeforeUninstall
        {
            add => _beforeUninstallHandler = (InstallEventHandler)Delegate.Combine(_beforeUninstallHandler, value);
            remove => _beforeUninstallHandler = (InstallEventHandler)Delegate.Remove(_beforeUninstallHandler, value);
        }

        internal bool InstallerTreeContains(Installer target)
        {
            if (Installers.Contains(target))
            {
                return true;
            }
            foreach (Installer installer in Installers)
            {
                if (installer.InstallerTreeContains(target))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>When overridden in a derived class, completes the install transaction.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after all the installers in the collection have run. </param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="savedState" /> parameter is null.-or- The saved-state <see cref="T:System.Collections.IDictionary" /> might have been corrupted. </exception>
        /// <exception cref="T:System.Configuration.Install.InstallException">An exception occurred during the <see cref="M:System.Configuration.Install.Installer.Commit(System.Collections.IDictionary)" /> phase of the installation. This exception is ignored and the installation continues. However, the application might not function correctly after the installation is complete. </exception>
        public virtual void Commit(IDictionary savedState)
        {
            if (savedState == null)
            {
                throw new ArgumentException(string.Format("{0} 不能为 null", "savedState"));
            }
            if (savedState["_reserved_lastInstallerAttempted"] == null || savedState["_reserved_nestedSavedStates"] == null)
            {
                throw new ArgumentException(string.Format("{0} 字典不包含预期值，可能已损坏", "savedState"));
            }
            Exception ex = null;
            try
            {
                OnCommitting(savedState);
            }
            catch (Exception ex2)
            {
                WriteEventHandlerError("Warning!", "OnCommitting", ex2);
                Context.LogMessage("在安装的提交阶段发生异常。此异常将被忽略，安装将继续。但是，安装完成后，应用程序可能无法正常运行");
                ex = ex2;
            }
            var num = Convert.ToInt32(savedState["_reserved_lastInstallerAttempted"]);
            var array = ToDictionaries(savedState["_reserved_nestedSavedStates"]);
            if (num + 1 != array.Length || num >= Installers.Count)
            {
                throw new ArgumentException(string.Format("{0} 字典包含不一致的数据，可能已损坏", "savedState"));
            }
            for (var i = 0; i < Installers.Count; i++)
            {
                Installers[i].Context = Context;
            }
            for (var j = 0; j <= num; j++)
            {
                try
                {
                    Installers[j].Commit(array[j]);
                }
                catch (Exception ex3)
                {
                    if (!IsWrappedException(ex3))
                    {
                        Context.LogMessage(string.Format("在 {0} 安装程序的提交阶段发生异常", Installers[j].ToString()));
                        LogException(ex3, Context);
                        Context.LogMessage("在安装的提交阶段发生异常。此异常将被忽略，安装将继续。但是，安装完成后，应用程序可能无法正常运行");
                    }
                    ex = ex3;
                }
            }
            savedState["_reserved_nestedSavedStates"] = array;
            savedState.Remove("_reserved_lastInstallerAttempted");
            try
            {
                OnCommitted(savedState);
            }
            catch (Exception ex4)
            {
                WriteEventHandlerError("Warning!", "OnCommitted", ex4);
                Context.LogMessage("在安装的提交阶段发生异常。此异常将被忽略，安装将继续。但是，安装完成后，应用程序可能无法正常运行");
                ex = ex4;
            }
            if (ex != null)
            {
                var ex5 = ex;
                if (!IsWrappedException(ex))
                {
                    ex5 = new InstallException("在安装的提交阶段发生异常。此异常将被忽略，安装将继续。但是，安装完成后，应用程序可能无法正常运行", ex);
                    ex5.Source = "WrappedExceptionSource";
                }
                throw ex5;
            }
        }

        /// <summary>When overridden in a derived class, performs the installation.</summary>
        /// <param name="stateSaver">An <see cref="T:System.Collections.IDictionary" /> used to save information needed to perform a commit, rollback, or uninstall operation. </param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="stateSaver" /> parameter is null. </exception>
        /// <exception cref="T:System.Exception">An exception occurred in the <see cref="E:System.Configuration.Install.Installer.BeforeInstall" /> event handler of one of the installers in the collection.-or- An exception occurred in the <see cref="E:System.Configuration.Install.Installer.AfterInstall" /> event handler of one of the installers in the collection. </exception>
        public virtual void Install(IDictionary stateSaver)
        {
            if (stateSaver == null)
            {
                throw new ArgumentException(string.Format("{0} 参数不能为null", "stateSaver"));
            }
            try
            {
                OnBeforeInstall(stateSaver);
            }
            catch (Exception ex)
            {
                WriteEventHandlerError("Error!", "OnBeforeInstall", ex);
                throw new InvalidOperationException(string.Format("{1} 的 {0} 事件处理程序中发生异常", "OnBeforeInstall", GetType().FullName), ex);
            }
            var num = -1;
            var arrayList = new ArrayList();
            try
            {
                for (var i = 0; i < Installers.Count; i++)
                {
                    Installers[i].Context = Context;
                }
                for (var j = 0; j < Installers.Count; j++)
                {
                    var installer = Installers[j];
                    IDictionary dictionary = new Hashtable();
                    try
                    {
                        num = j;
                        installer.Install(dictionary);
                    }
                    finally
                    {
                        arrayList.Add(dictionary);
                    }
                }
            }
            finally
            {
                stateSaver.Add("_reserved_lastInstallerAttempted", num);
                stateSaver.Add("_reserved_nestedSavedStates", arrayList.ToArray(typeof(IDictionary)));
            }
            try
            {
                OnAfterInstall(stateSaver);
            }
            catch (Exception ex2)
            {
                WriteEventHandlerError("Error!", "OnAfterInstall", ex2);
                throw new InvalidOperationException(string.Format("{1} 的 {0} 事件处理程序中发生异常", "OnAfterInstall", GetType().FullName), ex2);
            }
        }

        internal static void LogException(Exception e, InstallContext context)
        {
            var flag = true;
            while (e != null)
            {
                if (flag)
                {
                    context.LogMessage(e.GetType().FullName + ": " + e.Message);
                    flag = false;
                }
                else
                {
                    context.LogMessage(string.Format("引发内部异常 {0} ，并显示以下错误消息: {1}", e.GetType().FullName, e.Message));
                }
                if (context.IsParameterTrue("showcallstack"))
                {
                    context.LogMessage(e.StackTrace);
                }
                e = e.InnerException;
            }
        }

        private bool IsWrappedException(Exception e)
        {
            if (e is InstallException && e.Source == "WrappedExceptionSource")
            {
                return e.TargetSite.ReflectedType == typeof(Installer);
            }
            return false;
        }

        /// <summary>Raises the <see cref="E:System.Configuration.Install.Installer.Committed" /> event.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after all the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property run. </param>
        protected virtual void OnCommitted(IDictionary savedState)
        {
            _afterCommitHandler?.Invoke(this, new InstallEventArgs(savedState));
        }

        /// <summary>Raises the <see cref="E:System.Configuration.Install.Installer.AfterInstall" /> event.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after all the installers contained in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property have completed their installations. </param>
        protected virtual void OnAfterInstall(IDictionary savedState)
        {
            _afterInstallHandler?.Invoke(this, new InstallEventArgs(savedState));
        }

        /// <summary>Raises the <see cref="E:System.Configuration.Install.Installer.AfterRollback" /> event.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after the installers contained in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property are rolled back. </param>
        protected virtual void OnAfterRollback(IDictionary savedState)
        {
            _afterRollbackHandler?.Invoke(this, new InstallEventArgs(savedState));
        }

        /// <summary>Raises the <see cref="E:System.Configuration.Install.Installer.AfterUninstall" /> event.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after all the installers contained in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property are uninstalled. </param>
        protected virtual void OnAfterUninstall(IDictionary savedState)
        {
            _afterUninstallHandler?.Invoke(this, new InstallEventArgs(savedState));
        }

        /// <summary>Raises the <see cref="E:System.Configuration.Install.Installer.Committing" /> event.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer before the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property are committed. </param>
        protected virtual void OnCommitting(IDictionary savedState)
        {
            _beforeCommitHandler?.Invoke(this, new InstallEventArgs(savedState));
        }

        /// <summary>Raises the <see cref="E:System.Configuration.Install.Installer.BeforeInstall" /> event.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer before the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property are installed. This <see cref="T:System.Collections.IDictionary" /> object should be empty at this point. </param>
        protected virtual void OnBeforeInstall(IDictionary savedState)
        {
            _beforeInstallHandler?.Invoke(this, new InstallEventArgs(savedState));
        }

        /// <summary>Raises the <see cref="E:System.Configuration.Install.Installer.BeforeRollback" /> event.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer before the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property are rolled back. </param>
        protected virtual void OnBeforeRollback(IDictionary savedState)
        {
            _beforeRollbackHandler?.Invoke(this, new InstallEventArgs(savedState));
        }

        /// <summary>Raises the <see cref="E:System.Configuration.Install.Installer.BeforeUninstall" /> event.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer before the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property uninstall their installations. </param>
        protected virtual void OnBeforeUninstall(IDictionary savedState)
        {
            _beforeUninstallHandler?.Invoke(this, new InstallEventArgs(savedState));
        }

        /// <summary>When overridden in a derived class, restores the pre-installation state of the computer.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the pre-installation state of the computer. </param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="savedState" /> parameter is null.-or- The saved-state <see cref="T:System.Collections.IDictionary" /> might have been corrupted. </exception>
        /// <exception cref="T:System.Configuration.Install.InstallException">An exception occurred during the <see cref="M:System.Configuration.Install.Installer.Rollback(System.Collections.IDictionary)" /> phase of the installation. This exception is ignored and the rollback continues. However, the computer might not be fully reverted to its initial state after the rollback completes. </exception>
        public virtual void Rollback(IDictionary savedState)
        {
            if (savedState == null)
            {
                throw new ArgumentException(string.Format("{0} 不能为null", "savedState"));
            }
            if (savedState["_reserved_lastInstallerAttempted"] == null || savedState["_reserved_nestedSavedStates"] == null)
            {
                throw new ArgumentException(string.Format("{0} 字典不包含预期值，可能已损坏", "savedState"));
            }
            Exception ex = null;
            try
            {
                OnBeforeRollback(savedState);
            }
            catch (Exception ex2)
            {
                WriteEventHandlerError("Warning!", "OnBeforeRollback", ex2);
                Context.LogMessage("在安装的回滚阶段发生异常。此异常将被忽略，回滚将继续。但是，回滚完成后，计算机可能无法完全恢复到其初始状态");
                ex = ex2;
            }
            var num = Convert.ToInt32(savedState["_reserved_lastInstallerAttempted"]);
            var array = ToDictionaries(savedState["_reserved_nestedSavedStates"]);
            if (num + 1 != array.Length || num >= Installers.Count)
            {
                throw new ArgumentException(string.Format("{0} 字典包含不一致的数据，可能已损坏", "savedState"));
            }
            for (var num2 = Installers.Count - 1; num2 >= 0; num2--)
            {
                Installers[num2].Context = Context;
            }
            for (var num3 = num; num3 >= 0; num3--)
            {
                try
                {
                    Installers[num3].Rollback(array[num3]);
                }
                catch (Exception ex3)
                {
                    if (!IsWrappedException(ex3))
                    {
                        Context.LogMessage(string.Format("在 {0} 安装程序的回滚阶段发生异常", Installers[num3].ToString()));
                        LogException(ex3, Context);
                        Context.LogMessage("在安装的回滚阶段发生异常。此异常将被忽略，回滚将继续。但是，回滚完成后，计算机可能无法完全恢复到其初始状态");
                    }
                    ex = ex3;
                }
            }
            try
            {
                OnAfterRollback(savedState);
            }
            catch (Exception ex4)
            {
                WriteEventHandlerError("Warning!", "OnAfterRollback", ex4);
                Context.LogMessage("在安装的回滚阶段发生异常。此异常将被忽略，回滚将继续。但是，回滚完成后，计算机可能无法完全恢复到其初始状态");
                ex = ex4;
            }
            if (ex != null)
            {
                var ex5 = ex;
                if (!IsWrappedException(ex))
                {
                    ex5 = new InstallException("在安装的回滚阶段发生异常。此异常将被忽略，回滚将继续。但是，回滚完成后，计算机可能无法完全恢复到其初始状态", ex);
                    ex5.Source = "WrappedExceptionSource";
                }
                throw ex5;
            }
        }


        /// <summary>When overridden in a derived class, removes an installation.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after the installation was complete. </param>
        /// <exception cref="T:System.ArgumentException">The saved-state <see cref="T:System.Collections.IDictionary" /> might have been corrupted. </exception>
        /// <exception cref="T:System.Configuration.Install.InstallException">An exception occurred while uninstalling. This exception is ignored and the uninstall continues. However, the application might not be fully uninstalled after the uninstallation completes. </exception>
        public virtual void Uninstall(IDictionary savedState)
        {
            Exception ex = null;
            try
            {
                OnBeforeUninstall(savedState);
            }
            catch (Exception ex2)
            {
                WriteEventHandlerError("Warning!", "OnBeforeUninstall", ex2);
                Context.LogMessage("卸载时发生异常。此异常将被忽略，卸载将继续。但是，卸载完成后，应用程序可能无法完全卸载");
                ex = ex2;
            }
            IDictionary[] array;
            if (savedState != null)
            {
                array = ToDictionaries(savedState["_reserved_nestedSavedStates"]);
                if (array == null || array.Length != Installers.Count)
                {
                    throw new ArgumentException(string.Format("{0} 字典包含不一致的数据，可能已损坏", "savedState"));
                }
            }
            else
            {
                array = new IDictionary[Installers.Count];
            }
            for (var num = Installers.Count - 1; num >= 0; num--)
            {
                Installers[num].Context = Context;
            }
            for (var num2 = Installers.Count - 1; num2 >= 0; num2--)
            {
                try
                {
                    Installers[num2].Uninstall(array[num2]);
                }
                catch (Exception ex3)
                {
                    if (!IsWrappedException(ex3))
                    {
                        Context.LogMessage(string.Format("卸载 {0} 安装程序时发生异常", Installers[num2].ToString()));
                        LogException(ex3, Context);
                        Context.LogMessage("卸载时发生异常。此异常将被忽略，卸载将继续。但是，卸载完成后，应用程序可能无法完全卸载");
                    }
                    ex = ex3;
                }
            }
            try
            {
                OnAfterUninstall(savedState);
            }
            catch (Exception ex4)
            {
                WriteEventHandlerError("Warning!", "OnAfterUninstall", ex4);
                Context.LogMessage("卸载时发生异常。此异常将被忽略，卸载将继续。但是，卸载完成后，应用程序可能无法完全卸载");
                ex = ex4;
            }
            if (ex != null)
            {
                var ex5 = ex;
                if (!IsWrappedException(ex))
                {
                    ex5 = new InstallException("卸载时发生异常。此异常将被忽略，卸载将继续。但是，卸载完成后，应用程序可能无法完全卸载", ex);
                    ex5.Source = "WrappedExceptionSource";
                }
                throw ex5;
            }
        }

        private static IDictionary[] ToDictionaries(object savedState)
        {
            return (IDictionary[])savedState;
        }

        private void WriteEventHandlerError(string severity, string eventName, Exception e)
        {
            Context.LogMessage(string.Format("{2} 的 {1} 事件处理程序中发生异常", severity, eventName, GetType().FullName));
            LogException(e, Context);
        }
    }
}
