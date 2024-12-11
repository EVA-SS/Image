using System.Collections;

namespace System.Configuration.Install
{
    /// <summary>Defines an installer that either succeeds completely or fails and leaves the computer in its initial state.</summary>
    public class TransactedInstaller : Installer
    {
        /// <summary>Performs the installation.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> in which this method saves information needed to perform a commit, rollback, or uninstall operation. </param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="savedState" /> parameter is null. </exception>
        /// <exception cref="T:System.Exception">The installation failed, and is being rolled back. </exception>
        public override void Install(IDictionary savedState)
        {
            if (Context == null)
            {
                Context = new InstallContext();
            }
            Context.LogMessage(Environment.NewLine + "运行事务处理安装");
            try
            {
                var flag = true;
                try
                {
                    Context.LogMessage(Environment.NewLine + "开始安装的安装阶段");
                    base.Install(savedState);
                }
                catch (Exception ex)
                {
                    flag = false;
                    Context.LogMessage(Environment.NewLine + "安装阶段发生异常");
                    LogException(ex, Context);
                    Context.LogMessage(Environment.NewLine + "安装的回滚阶段即将开始");
                    try
                    {
                        Rollback(savedState);
                    }
                    catch (Exception)
                    {
                    }
                    Context.LogMessage(Environment.NewLine + "回滚阶段已成功完成");
                    throw new InvalidOperationException("安装失败，已执行回滚", ex);
                }
                if (flag)
                {
                    Context.LogMessage(Environment.NewLine + "安装阶段已成功完成，提交阶段正在开始");
                    try
                    {
                        Commit(savedState);
                    }
                    finally
                    {
                        Context.LogMessage(Environment.NewLine + "提交阶段已成功完成");
                    }
                }
            }
            finally
            {
                Context.LogMessage(Environment.NewLine + "交易安装已完成");
            }
        }

        /// <summary>Removes an installation.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after the installation completed. </param>
        public override void Uninstall(IDictionary savedState)
        {
            if (Context == null) Context = new InstallContext();
            Context.LogMessage(Environment.NewLine + Environment.NewLine + "卸载正在开始");
            try
            {
                base.Uninstall(savedState);
            }
            finally
            {
                Context.LogMessage(Environment.NewLine + "卸载已完成");
            }
        }
    }
}