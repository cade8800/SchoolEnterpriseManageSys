using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SchoolEnterpriseManageSys.Enum
{
    public enum ProjectType : int
    {
        /// <summary>
        /// 校外基地
        /// </summary>
        [Description("校外实践基地")]
        OffCampusBase = 2,
        /// <summary>
        /// 校内基地
        /// </summary>
        [Description("校内共建基地")]
        CampusBase = 4,
        /// <summary>
        /// 社会服务
        /// </summary>
        [Description("社会服务")]
        SocialService = 8,
        /// <summary>
        /// 订单培养
        /// </summary>
        [Description("订单培养")]
        OrderTraining = 16,
        /// <summary>
        /// 共同编写的教材或课程
        /// </summary>
        [Description("共编教材/课程")]
        CoAuthoredBookOrCourse = 32,
        /// <summary>
        /// 教学研究基金
        /// </summary>
        [Description("教学研基金")]
        TeachingResearchFund = 64,
        /// <summary>
        /// 学术成果
        /// </summary>
        [Description("学术成果")]
        AcademicAchievement = 128,
        /// <summary>
        /// 共同建立的专业 后改为文件管理
        /// </summary>
        [Description("文件管理")]
        JointlyEstablishedProfession = 256,
    }
}
