using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Constants;

public enum Permissions : ushort //Must be ushort to work with AuthP
{
    NotSet = 0, //error condition

    [Display(GroupName = "TodoLists", Name = "Create", Description = "Can Create Todo List")]
    CreateTodoList = 10,

    [Display(GroupName = "TodoLists", Name = "GetTodos", Description = "Can view Todo List")]
    GetTodos = 11,

    [Display(GroupName = "TodoLists", Name = "Update", Description = "Can Update Todo List")]
    UpdateTodoList = 12,

    [Display(GroupName = "TodoLists", Name = "Update", Description = "Can Update Todo List")]
    DeleteTodoList = 13,

    [Display(GroupName = "TodoLists", Name = "Purge", Description = "Can Purge Todo List")]
    PurgeTodoLists = 14,

    [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature", AutoGenerateFilter = true)]
    AccessAll = ushort.MaxValue,
}
