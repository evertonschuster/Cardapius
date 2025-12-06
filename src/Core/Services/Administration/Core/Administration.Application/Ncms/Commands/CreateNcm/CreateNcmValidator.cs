using Administration.Application.Ncms.Commands;
using Administration.Domain.Ncms.ValueObjects;
using FluentValidation;

namespace Administration.Application.Ncms.Commands.CreateNcm;

public class CreateNcmValidator : NcmCommandValidator<CreateNcmCommand>;
