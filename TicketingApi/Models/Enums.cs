using System.Runtime.Serialization;

namespace TicketingApi.Models;

public enum Status
{
	Backlog,
	Scoping,
	[EnumMember(Value = "In Design")]
	InDesign,
	[EnumMember(Value = "In Development")]
	InDevelopment,
	[EnumMember(Value = "In Review")]
	InReview,
	Testing,
	[EnumMember(Value = "Ready For Development")]
	ReadyForDevelopment,
	Shipped,
	Cancelled
}

public enum Priority
{
	Urgent,
	High,
	Normal,
	Low
}