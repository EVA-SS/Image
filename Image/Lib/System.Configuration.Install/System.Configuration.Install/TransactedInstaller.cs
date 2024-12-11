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
            Context.LogMessage(Environment.NewLine + "����������װ");
            try
            {
                var flag = true;
                try
                {
                    Context.LogMessage(Environment.NewLine + "��ʼ��װ�İ�װ�׶�");
                    base.Install(savedState);
                }
                catch (Exception ex)
                {
                    flag = false;
                    Context.LogMessage(Environment.NewLine + "��װ�׶η����쳣");
                    LogException(ex, Context);
                    Context.LogMessage(Environment.NewLine + "��װ�Ļع��׶μ�����ʼ");
                    try
                    {
                        Rollback(savedState);
                    }
                    catch (Exception)
                    {
                    }
                    Context.LogMessage(Environment.NewLine + "�ع��׶��ѳɹ����");
                    throw new InvalidOperationException("��װʧ�ܣ���ִ�лع�", ex);
                }
                if (flag)
                {
                    Context.LogMessage(Environment.NewLine + "��װ�׶��ѳɹ���ɣ��ύ�׶����ڿ�ʼ");
                    try
                    {
                        Commit(savedState);
                    }
                    finally
                    {
                        Context.LogMessage(Environment.NewLine + "�ύ�׶��ѳɹ����");
                    }
                }
            }
            finally
            {
                Context.LogMessage(Environment.NewLine + "���װ�װ�����");
            }
        }

        /// <summary>Removes an installation.</summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after the installation completed. </param>
        public override void Uninstall(IDictionary savedState)
        {
            if (Context == null) Context = new InstallContext();
            Context.LogMessage(Environment.NewLine + Environment.NewLine + "ж�����ڿ�ʼ");
            try
            {
                base.Uninstall(savedState);
            }
            finally
            {
                Context.LogMessage(Environment.NewLine + "ж�������");
            }
        }
    }
}