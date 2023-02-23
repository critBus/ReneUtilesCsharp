using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles
{
    public abstract class UtilesHardware
    {
        public static string GetMotherBoardID()
        {
            string mbInfo = String.Empty;
            ManagementScope scope = new ManagementScope("\\\\" + Environment.MachineName + "\\root\\cimv2");
            scope.Connect();
            ManagementObject wmiClass = new ManagementObject(scope, new ManagementPath("Win32_BaseBoard.Tag=\"Base Board\""), new ObjectGetOptions());

            foreach (PropertyData propData in wmiClass.Properties)
            {
                if (propData.Name == "SerialNumber") {
                    mbInfo = Convert.ToString(propData.Value);
                    return mbInfo;
                }
                    //mbInfo = String.Format("{0,-25}{1}", propData.Name, Convert.ToString(propData.Value));
            }

            return mbInfo;
        }
    }
}
