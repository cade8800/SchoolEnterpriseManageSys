using System;

namespace SchoolEnterpriseManageSys.Utilities.EnumHelper
{
    public struct EnumModel
    {
        public EnumModel(Enum um)
        {
            this.value = (int)Convert.ChangeType(um, typeof(int));
            this.name = um.ToString();
            this.text = um.GetDescription();
        }
        public int value { get; set; }
        public string name { get; set; }
        public string text { get; set; }
    }
}
