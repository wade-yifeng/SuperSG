

// This file is auto-generated, do not modify this file.
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Sleemon.Data
{
    public sealed class SleemonEntities : DbContext, ISleemonEntities
    {
        public const string ConnectionString = "name=SleemonEntities";
        public const string ContainerName = "SleemonEntities";
    
    	public void Detach(object obj)
    	{
    		Entry(obj).State = EntityState.Detached;
    	}
    	
    	public void DiscardChanges()
    	{
    		_Context.DiscardChanges();
    	}
    
    	
    	private ObjectContext _Context
    	{
    		get { return ((IObjectContextAdapter)this).ObjectContext; }
    	}
    	
    
        #region Constructors
    
        public SleemonEntities()
            : this(ConnectionString)
        {
        }
    
        public SleemonEntities(string connectionString, int? commandTimeout = null)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
    		_Context.CommandTimeout = commandTimeout ?? 120;
    		Configuration.ValidateOnSaveEnabled = false; 
        }
    
        #endregion
    	
    	bool ISleemonEntities.CanPreCompile
    	{
    		get { return true; }
    	}
    
        #region DbSet Properties
        public IDbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public IDbSet<ConsEnterpriseNotice> ConsEnterpriseNotice { get; set; }
        public IDbSet<Department> Department { get; set; }
        public IDbSet<DepartmentEnterpriseNotice> DepartmentEnterpriseNotice { get; set; }
        public IDbSet<EnterpriseNotice> EnterpriseNotice { get; set; }
        public IDbSet<Exam> Exam { get; set; }
        public IDbSet<ExamChoice> ExamChoice { get; set; }
        public IDbSet<ExamQuestion> ExamQuestion { get; set; }
        public IDbSet<GroupSubTask> GroupSubTask { get; set; }
        public IDbSet<GroupTask> GroupTask { get; set; }
        public IDbSet<Keyword> Keyword { get; set; }
        public IDbSet<KnowledgeBase> KnowledgeBase { get; set; }
        public IDbSet<KnowledgeBaseKeyword> KnowledgeBaseKeyword { get; set; }
        public IDbSet<LearningChapter> LearningChapter { get; set; }
        public IDbSet<LearningCourse> LearningCourse { get; set; }
        public IDbSet<LearningFile> LearningFile { get; set; }
        public IDbSet<MessageDispatch> MessageDispatch { get; set; }
        public IDbSet<MessageReceiver> MessageReceiver { get; set; }
        public IDbSet<OrderShowFile> OrderShowFile { get; set; }
        public IDbSet<Permission> Permission { get; set; }
        public IDbSet<ProsComments> ProsComments { get; set; }
        public IDbSet<ProsEnterpriseNotice> ProsEnterpriseNotice { get; set; }
        public IDbSet<Questionnaire> Questionnaire { get; set; }
        public IDbSet<QuestionnaireItem> QuestionnaireItem { get; set; }
        public IDbSet<Role> Role { get; set; }
        public IDbSet<RolePermission> RolePermission { get; set; }
        public IDbSet<SleemonExceptionLog> SleemonExceptionLog { get; set; }
        public IDbSet<StorePatrol> StorePatrol { get; set; }
        public IDbSet<SystemConfig> SystemConfig { get; set; }
        public IDbSet<Task> Task { get; set; }
        public IDbSet<TaskExam> TaskExam { get; set; }
        public IDbSet<TaskLearning> TaskLearning { get; set; }
        public IDbSet<TaskQuestionnaire> TaskQuestionnaire { get; set; }
        public IDbSet<Training> Training { get; set; }
        public IDbSet<TrainingTask> TrainingTask { get; set; }
        public IDbSet<User> User { get; set; }
        public IDbSet<UserComments> UserComments { get; set; }
        public IDbSet<UserDailySignIn> UserDailySignIn { get; set; }
        public IDbSet<UserDepartment> UserDepartment { get; set; }
        public IDbSet<UserExamAnswer> UserExamAnswer { get; set; }
        public IDbSet<UserMoments> UserMoments { get; set; }
        public IDbSet<UserOrderShow> UserOrderShow { get; set; }
        public IDbSet<UserPointRecord> UserPointRecord { get; set; }
        public IDbSet<UserQuestion> UserQuestion { get; set; }
        public IDbSet<UserQuestionnaireAnswer> UserQuestionnaireAnswer { get; set; }
        public IDbSet<UserQuestionReply> UserQuestionReply { get; set; }
        public IDbSet<UserRole> UserRole { get; set; }
        public IDbSet<UserStorePatrol> UserStorePatrol { get; set; }
        public IDbSet<UserTask> UserTask { get; set; }
        public IDbSet<UserTraining> UserTraining { get; set; }

        #endregion

    
        #region Function Imports
    
    	public ObjectResult<spCommitEntireExam_Result> spCommitEntireExam(Nullable<int> userTaskId, string userId)
    	{
    		ObjectParameter userTaskIdParameter = userTaskId.HasValue ? new ObjectParameter("userTaskId", userTaskId) : new ObjectParameter("userTaskId", typeof(int));
    		ObjectParameter userIdParameter = userId != null ? new ObjectParameter("userId", userId) : new ObjectParameter("userId", typeof(string));
    		return _Context.ExecuteFunction<spCommitEntireExam_Result>("spCommitEntireExam", userTaskIdParameter, userIdParameter);
    	}
     
    	IEnumerable<spCommitEntireExam_Result>  ISleemonEntities.spCommitEntireExam(Nullable<int> userTaskId, string userId)
    	{
     		return  this.spCommitEntireExam(userTaskId, userId);	
    	}
    
    	public void spDeleteExamById(Nullable<int> examId)
    	{
    		ObjectParameter examIdParameter = examId.HasValue ? new ObjectParameter("examId", examId) : new ObjectParameter("examId", typeof(int));
    		_Context.ExecuteFunction("spDeleteExamById", examIdParameter);
    	}
    
    	public ObjectResult<spGetBroadcastMessage_Result> spGetBroadcastMessage(Nullable<int> maxCount)
    	{
    		ObjectParameter maxCountParameter = maxCount.HasValue ? new ObjectParameter("maxCount", maxCount) : new ObjectParameter("maxCount", typeof(int));
    		return _Context.ExecuteFunction<spGetBroadcastMessage_Result>("spGetBroadcastMessage", maxCountParameter);
    	}
     
    	IEnumerable<spGetBroadcastMessage_Result>  ISleemonEntities.spGetBroadcastMessage(Nullable<int> maxCount)
    	{
     		return  this.spGetBroadcastMessage(maxCount);	
    	}
    
    	public ObjectResult<spGetStorePatrolList_Result> spGetStorePatrolList(Nullable<int> pageIndex, Nullable<int> pageSize, string userName, Nullable<System.DateTime> startFrom, Nullable<System.DateTime> endTo)
    	{
    		ObjectParameter pageIndexParameter = pageIndex.HasValue ? new ObjectParameter("pageIndex", pageIndex) : new ObjectParameter("pageIndex", typeof(int));
    		ObjectParameter pageSizeParameter = pageSize.HasValue ? new ObjectParameter("pageSize", pageSize) : new ObjectParameter("pageSize", typeof(int));
    		ObjectParameter userNameParameter = userName != null ? new ObjectParameter("userName", userName) : new ObjectParameter("userName", typeof(string));
    		ObjectParameter startFromParameter = startFrom.HasValue ? new ObjectParameter("startFrom", startFrom) : new ObjectParameter("startFrom", typeof(System.DateTime));
    		ObjectParameter endToParameter = endTo.HasValue ? new ObjectParameter("endTo", endTo) : new ObjectParameter("endTo", typeof(System.DateTime));
    		return _Context.ExecuteFunction<spGetStorePatrolList_Result>("spGetStorePatrolList", pageIndexParameter, pageSizeParameter, userNameParameter, startFromParameter, endToParameter);
    	}
     
    	IEnumerable<spGetStorePatrolList_Result>  ISleemonEntities.spGetStorePatrolList(Nullable<int> pageIndex, Nullable<int> pageSize, string userName, Nullable<System.DateTime> startFrom, Nullable<System.DateTime> endTo)
    	{
     		return  this.spGetStorePatrolList(pageIndex, pageSize, userName, startFrom, endTo);	
    	}
    
    	public ObjectResult<spGetUserPermissions_Result> spGetUserPermissions(string userUniqueId)
    	{
    		ObjectParameter userUniqueIdParameter = userUniqueId != null ? new ObjectParameter("userUniqueId", userUniqueId) : new ObjectParameter("userUniqueId", typeof(string));
    		return _Context.ExecuteFunction<spGetUserPermissions_Result>("spGetUserPermissions", userUniqueIdParameter);
    	}
     
    	IEnumerable<spGetUserPermissions_Result>  ISleemonEntities.spGetUserPermissions(string userUniqueId)
    	{
     		return  this.spGetUserPermissions(userUniqueId);	
    	}
    
    	public void spPointStorePatrol(Nullable<bool> isPass, string userStorePatrol)
    	{
    		ObjectParameter isPassParameter = isPass.HasValue ? new ObjectParameter("isPass", isPass) : new ObjectParameter("isPass", typeof(bool));
    		ObjectParameter userStorePatrolParameter = userStorePatrol != null ? new ObjectParameter("userStorePatrol", userStorePatrol) : new ObjectParameter("userStorePatrol", typeof(string));
    		_Context.ExecuteFunction("spPointStorePatrol", isPassParameter, userStorePatrolParameter);
    	}
    
    	public void spSaveTrainingDetail(string training)
    	{
    		ObjectParameter trainingParameter = training != null ? new ObjectParameter("training", training) : new ObjectParameter("training", typeof(string));
    		_Context.ExecuteFunction("spSaveTrainingDetail", trainingParameter);
    	}
    
    	public void spSyncDepartment(string department)
    	{
    		ObjectParameter departmentParameter = department != null ? new ObjectParameter("department", department) : new ObjectParameter("department", typeof(string));
    		_Context.ExecuteFunction("spSyncDepartment", departmentParameter);
    	}
    
    	public void spSyncUser(string users)
    	{
    		ObjectParameter usersParameter = users != null ? new ObjectParameter("users", users) : new ObjectParameter("users", typeof(string));
    		_Context.ExecuteFunction("spSyncUser", usersParameter);
    	}
    
    	public void spSyncUserDepartment(string userDepartment)
    	{
    		ObjectParameter userDepartmentParameter = userDepartment != null ? new ObjectParameter("userDepartment", userDepartment) : new ObjectParameter("userDepartment", typeof(string));
    		_Context.ExecuteFunction("spSyncUserDepartment", userDepartmentParameter);
    	}
    
    	public void spUpdateTrainingUsersJoinState(Nullable<int> trainingId, string userJoinStatusEntities, string lastUpdateUser)
    	{
    		ObjectParameter trainingIdParameter = trainingId.HasValue ? new ObjectParameter("trainingId", trainingId) : new ObjectParameter("trainingId", typeof(int));
    		ObjectParameter userJoinStatusEntitiesParameter = userJoinStatusEntities != null ? new ObjectParameter("userJoinStatusEntities", userJoinStatusEntities) : new ObjectParameter("userJoinStatusEntities", typeof(string));
    		ObjectParameter lastUpdateUserParameter = lastUpdateUser != null ? new ObjectParameter("lastUpdateUser", lastUpdateUser) : new ObjectParameter("lastUpdateUser", typeof(string));
    		_Context.ExecuteFunction("spUpdateTrainingUsersJoinState", trainingIdParameter, userJoinStatusEntitiesParameter, lastUpdateUserParameter);
    	}

        #endregion

        public void SetEntryState<T>(T entry, EntityState state) where T : Entity
        {
            this.Entry(entry).State = state;
        }
    }
}



