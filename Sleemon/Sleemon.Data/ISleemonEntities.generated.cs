// Context interface. This file is auto-generated, do not modify this file.
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace Sleemon.Data
{
    public partial interface ISleemonEntities : IDisposable
    {
    	bool CanPreCompile { get; }
    	void Detach(object entity);
    	int SaveChanges();
    	void DiscardChanges();
        #region DbSet Properties
        IDbSet<C__RefactorLog> C__RefactorLog { get; set; }
        IDbSet<ConsEnterpriseNotice> ConsEnterpriseNotice { get; set; }
        IDbSet<Department> Department { get; set; }
        IDbSet<DepartmentEnterpriseNotice> DepartmentEnterpriseNotice { get; set; }
        IDbSet<EnterpriseNotice> EnterpriseNotice { get; set; }
        IDbSet<Exam> Exam { get; set; }
        IDbSet<ExamChoice> ExamChoice { get; set; }
        IDbSet<ExamQuestion> ExamQuestion { get; set; }
        IDbSet<GroupSubTask> GroupSubTask { get; set; }
        IDbSet<GroupTask> GroupTask { get; set; }
        IDbSet<Keyword> Keyword { get; set; }
        IDbSet<KnowledgeBase> KnowledgeBase { get; set; }
        IDbSet<KnowledgeBaseKeyword> KnowledgeBaseKeyword { get; set; }
        IDbSet<LearningChapter> LearningChapter { get; set; }
        IDbSet<LearningCourse> LearningCourse { get; set; }
        IDbSet<LearningFile> LearningFile { get; set; }
        IDbSet<MessageDispatch> MessageDispatch { get; set; }
        IDbSet<MessageReceiver> MessageReceiver { get; set; }
        IDbSet<OrderShowFile> OrderShowFile { get; set; }
        IDbSet<Permission> Permission { get; set; }
        IDbSet<ProsComments> ProsComments { get; set; }
        IDbSet<ProsEnterpriseNotice> ProsEnterpriseNotice { get; set; }
        IDbSet<Questionnaire> Questionnaire { get; set; }
        IDbSet<QuestionnaireChoice> QuestionnaireChoice { get; set; }
        IDbSet<QuestionnaireItem> QuestionnaireItem { get; set; }
        IDbSet<Role> Role { get; set; }
        IDbSet<RolePermission> RolePermission { get; set; }
        IDbSet<SleemonExceptionLog> SleemonExceptionLog { get; set; }
        IDbSet<StorePatrol> StorePatrol { get; set; }
        IDbSet<SystemConfig> SystemConfig { get; set; }
        IDbSet<Task> Task { get; set; }
        IDbSet<TaskExam> TaskExam { get; set; }
        IDbSet<TaskLearning> TaskLearning { get; set; }
        IDbSet<TaskQuestionnaire> TaskQuestionnaire { get; set; }
        IDbSet<Training> Training { get; set; }
        IDbSet<TrainingTask> TrainingTask { get; set; }
        IDbSet<User> User { get; set; }
        IDbSet<UserComments> UserComments { get; set; }
        IDbSet<UserDailySignIn> UserDailySignIn { get; set; }
        IDbSet<UserDepartment> UserDepartment { get; set; }
        IDbSet<UserExamAnswer> UserExamAnswer { get; set; }
        IDbSet<UserMoments> UserMoments { get; set; }
        IDbSet<UserOrderShow> UserOrderShow { get; set; }
        IDbSet<UserPointRecord> UserPointRecord { get; set; }
        IDbSet<UserQuestion> UserQuestion { get; set; }
        IDbSet<UserQuestionnaireAnswer> UserQuestionnaireAnswer { get; set; }
        IDbSet<UserRole> UserRole { get; set; }
        IDbSet<UserStorePatrol> UserStorePatrol { get; set; }
        IDbSet<UserTask> UserTask { get; set; }
        IDbSet<UserTraining> UserTraining { get; set; }

        #endregion

        #region Function Imports
        IEnumerable<spCommitEntireExam_Result> spCommitEntireExam(Nullable<int> userTaskId, string userId);
    
    	void spDeleteCourseById(Nullable<int> courseId);
    
    	void spDeleteExamById(Nullable<int> examId);
    
    	void spDeleteQuestionnaireById(Nullable<int> questionnaireId);
    
        IEnumerable<spGetBroadcastMessage_Result> spGetBroadcastMessage(Nullable<int> maxCount);
    
        IEnumerable<spGetStorePatrolList_Result> spGetStorePatrolList(Nullable<int> pageIndex, Nullable<int> pageSize, string userName, Nullable<System.DateTime> startFrom, Nullable<System.DateTime> endTo);
    
        IEnumerable<spGetUserPermissions_Result> spGetUserPermissions(string userUniqueId);
    
    	void spPointStorePatrol(Nullable<bool> isPass, string userStorePatrol);
    
    	void spSaveTrainingDetail(string training);
    
    	void spSyncDepartment(string department);
    
    	void spSyncUser(string users);
    
    	void spSyncUserDepartment(string userDepartment);
    
    	void spUpdateTrainingUsersJoinState(Nullable<int> trainingId, string userJoinStatusEntities, string lastUpdateUser);
    

        #endregion

        Database Database { get; }
    
        void SetEntryState<T>(T entry, EntityState state) where T : Entity;
    }
}

