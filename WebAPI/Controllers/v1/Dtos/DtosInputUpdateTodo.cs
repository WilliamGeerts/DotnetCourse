﻿namespace WebAPI.Controllers.v1.Dtos;

public class DtosInputUpdateTodo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
}