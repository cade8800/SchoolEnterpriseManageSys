using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamir.SharpSsh.java.lang;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class StringBuilderEx
    { 
        StringBuilder sbMsg = new StringBuilder();
        private static readonly object LockObject = new object();
        public void AppendLine(string s)
        {
            try
            {
                //lock (LockObject)
                {
                    sbMsg.AppendLine(s);
                }
            }
            catch// (Exception ex)
            {

            }

        }

        public void Append(string s)
        {
            try
            {
                lock (LockObject)
                {
                    sbMsg.Append(s);
                }
            }
            catch// (Exception ex)
            {

            }

        }

        public void Clear()
        {
            try
            {
                lock (LockObject)
                {
                    sbMsg.Clear();
                }
            }
            catch// (Exception ex)
            {

            }

        }

        public new string ToString()
        {
            try
            {
                return sbMsg.ToString();
            }
            catch (Exception)
            {
                return "StringBuilderEx ToString() Exception";
            }

        }

        public void AppendFormat(string s, params object[] args)
        {
            try
            {
                lock (LockObject)
                {
                    sbMsg.AppendFormat(s, args);
                }
            }
            catch (Exception)
            { }
        }
    }
}
