export const Host = 'http://localhost:6234';

export const User_Login = Host + '/oauth2/token';
export const User_Update_Password = Host + '/api/services/app/userService/UpdatePassword';
export const User_Info = Host + '/api/services/app/userService/GetUserInfo';
export const User_Update_Info = Host + '/api/services/app/userService/UpdateUserInfo';
export const User_Enterprise_Regist = Host + '/api/services/app/userService/EnterpriseRegist';
export const User_GetUsers = Host + '/api/services/app/userService/GetUsers';
export const User_Update_State = Host + '/api/services/app/userService/UpdateUserState?UserId=';
export const User_Reset_Password = Host + '/api/services/app/userService/ResetUserPassword?UserId=';
export const User_Insert_Campust_User = Host + '/api/services/app/userService/InsertCampustUser';
export const User_Update_Avatar = Host + '/api/services/app/userService/UpdateAvatar';


export const App_Date = Host + '/api/services/app/menuService/GetAppDate';


export const Department_Get = Host + '/api/services/app/departmentService/Get';
export const Department_Delete = Host + '/api/services/app/departmentService/Delete?id=';
export const Department_Update = Host + '/api/services/app/departmentService/Update';
export const Department_Add = Host + '/api/services/app/departmentService/Add';


export const Project_Type_Get = Host + '/api/services/app/projectTypeService/Get';
export const Project_Type_Update = Host + '/api/services/app/projectTypeService/Update';
export const Project_Type_Detail = Host + '/api/services/app/projectTypeService/GetDetail?type=';


export const Project_Get_List = Host + '/api/services/app/projectService/GetProjectList';
export const Project_Detail = Host + '/api/services/app/projectService/GetProjectDetail';
export const Project_Delete = Host + '/api/services/app/projectService/DeleteProject?Id=';

export const Project_InsertAcademicAchievement = Host + '/api/services/app/projectService/InsertAcademicAchievement';
export const Project_UpdateAcademicAchievement = Host + '/api/services/app/projectService/UpdateAcademicAchievement';
export const Project_ImportAcademicAchievement = Host + '/api/services/app/projectService/ImportAcademicAchievement';

export const Project_InsertCoAuthoredBookOrCourse = Host + '/api/services/app/projectService/InsertCoAuthoredBookOrCourse';
export const Project_UpdateCoAuthoredBookOrCourse = Host + '/api/services/app/projectService/UpdateCoAuthoredBookOrCourse';
export const Project_ImportCoAuthoredBookOrCourse = Host + '/api/services/app/projectService/ImportCoAuthoredBookOrCourse';

export const Project_InsertCampusBase = Host + '/api/services/app/projectService/InsertCampusBase';
export const Project_UpdateCampusBase = Host + '/api/services/app/projectService/UpdateCampusBase';
export const Project_ImportCampusBase = Host + '/api/services/app/projectService/ImportCampusBase';

export const Project_InsertJointlyEstablishedProfession = Host + '/api/services/app/projectService/InsertJointlyEstablishedProfession';
export const Project_UpdateJointlyEstablishedProfession = Host + '/api/services/app/projectService/UpdateJointlyEstablishedProfession';

export const Project_InsertOffCampusBase = Host + '/api/services/app/projectService/InsertOffCampusBase';
export const Project_UpdateOffCampusBase = Host + '/api/services/app/projectService/UpdateOffCampusBase';
export const Project_ImportOffCampusBase = Host + '/api/services/app/projectService/ImportOffCampusBase';

export const Project_InsertOrderTraining = Host + '/api/services/app/projectService/InsertOrderTraining';
export const Project_UpdateOrderTraining = Host + '/api/services/app/projectService/UpdateOrderTraining';
export const Project_ImportOrderTraining = Host + '/api/services/app/projectService/ImportOrderTraining';

export const Project_InsertSocialService = Host + '/api/services/app/projectService/InsertSocialService';
export const Project_UpdateSocialService = Host + '/api/services/app/projectService/UpdateSocialService';
export const Project_ImportSocialService = Host + '/api/services/app/projectService/ImportSocialService';

export const Project_InsertTeachingResearchFund = Host + '/api/services/app/projectService/InsertTeachingResearchFund';
export const Project_UpdateTeachingResearchFund = Host + '/api/services/app/projectService/UpdateTeachingResearchFund';
export const Project_ImportTeachingResearchFund = Host + '/api/services/app/projectService/ImportTeachingResearchFund';

export const Project_Summary = Host + '/api/services/app/projectService/GetSummary';
export const Project_SelectSummary = Host + '/api/services/app/projectService/SelectSummary';


export const File_Upload = Host + '/api/File';
export const File_Delete = Host + '/api/services/app/fileService/DeleteFile?fileId=';
export const File_Delete_Project = Host + '/api/services/app/fileService/DeleteProjectFile';
export const File_Insert_Project = Host + '/api/services/app/fileService/InsertProjectFile';
export const File_Delete_Collect = Host + '/api/services/app/fileService/DeleteCollectFile';
export const File_Insert_Collect = Host + '/api/services/app/fileService/InsertCollectFile';
export const File_Delete_Enterprise = Host + '/api/services/app/fileService/DeleteEnterpriseFile';
export const File_Insert_Enterprise = Host + '/api/services/app/fileService/InsertEnterpriseFile';
export const File_Delete_Assessment = Host + '/api/services/app/fileService/DeleteAssessmentFile';
export const File_Insert_Assessment = Host + '/api/services/app/fileService/InsertAssessmentFile';


export const Enterprise_Get = Host + '/api/services/app/enterpriseService/Get';
export const Enterprise_GetDetail = Host + '/api/services/app/enterpriseService/GetDetail';
export const Enterprise_Edit = Host + '/api/services/app/enterpriseService/Edit';


export const Collect_Insert = Host + '/api/services/app/collectService/InsertCollect';
export const Collect_Update = Host + '/api/services/app/collectService/UpdateCollect';
export const Collect_Get = Host + '/api/services/app/collectService/GetCollect';
export const Collect_Get_Detail = Host + '/api/services/app/collectService/GetCollectDetail';
export const Collect_GetDepartmentCollectDetail = Host + '/api/services/app/collectService/GetDepartmentCollectDetail';
export const Collect_InsertDepartmentCollect = Host + '/api/services/app/collectService/InsertDepartmentCollect';
export const Collect_UpdateDepartmentCollectCooperation = Host + '/api/services/app/collectService/UpdateDepartmentCollectCooperation';
export const Collect_GetDepartmentCollectList = Host + '/api/services/app/collectService/GetDepartmentCollectList';
export const Collect_DeleteDepartmentCollectBase = Host + '/api/services/app/collectService/DeleteDepartmentCollectBase?id=';
export const Collect_UpdateDepartmentCollectBase = Host + '/api/services/app/collectService/UpdateDepartmentCollectBase';
export const Collect_InsertDepartmentCollectBase = Host + '/api/services/app/collectService/InsertDepartmentCollectBase';
export const Collect_GetDepartmentCollecdtItemFileList = Host + '/api/services/app/collectService/GetDepartmentCollecdtItemFileList';


export const Advisory_Get = Host + '/api/services/app/advisoryService/Get';
export const Advisory_Insert = Host + '/api/services/app/advisoryService/Insert';
export const Advisory_Get_Enterprise = Host + '/api/services/app/advisoryService/GetEnterpriseAdvisory';


export const Appointment_Get = Host + '/api/services/app/appointmentService/Get';
export const Appointment_Edit = Host + '/api/services/app/appointmentService/EditAppointment';
export const Appointment_Delete = Host + '/api/services/app/appointmentService/Delete?input.id=';
export const Appointment_Get_Enterprise = Host + '/api/services/app/appointmentService/GetEnterpriseAppointment';
export const Appointment_Confirm = Host + '/api/services/app/appointmentService/Confirm?input.id=';
export const Appointment_Send = Host + '/api/services/app/appointmentService/SendToDepartment';

export const Assessment_GetIndexs = Host + '/api/services/app/assessmentService/GetIndexs';
export const Assessment_DeleteIndex = Host + '/api/services/app/assessmentService/DeleteIndex?input.id=';
export const Assessment_GetIndex = Host + '/api/services/app/assessmentService/GetIndex';
export const Assessment_EditIndex = Host + '/api/services/app/assessmentService/EditIndex';
export const Assessment_GetAssessments = Host + '/api/services/app/assessmentService/GetAssessments';
export const Assessment_EditAssessment = Host + '/api/services/app/assessmentService/EditAssessment';
export const Assessment_GetAssessment = Host + '/api/services/app/assessmentService/GetAssessment';
export const Assessment_EditAssessmentDepartment = Host + '/api/services/app/assessmentService/EditAssessmentDepartment';
export const Assessment_GetAssessmentDepartmentList = Host + '/api/services/app/assessmentService/GetAssessmentDepartmentList';
export const Assessment_GetAssessmentDepartmentIndexList = Host + '/api/services/app/assessmentService/GetAssessmentDepartmentIndexList';
export const Assessment_SelfEvaluation = Host + '/api/services/app/assessmentService/SelfEvaluation';
export const Assessment_ExpertRating = Host + '/api/services/app/assessmentService/ExpertRating';
export const Assessment_GetAssessmentDepartmentIndex = Host + '/api/services/app/assessmentService/GetAssessmentDepartmentIndex';
export const Assessment_GetAssessmentDepartmentProjects = Host + '/api/services/app/assessmentService/GetAssessmentDepartmentProjects';
