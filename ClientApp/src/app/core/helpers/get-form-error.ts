import {ValidationErrors} from '@angular/forms';

export const getFormError = (error: ValidationErrors | undefined): string => {
  if (!error) return '';

  const errorKey = Object.keys(error)[0];

  const errorsMap = new Map<string, string>([
    ["required", "This field is required"],
    ["email", "This field must be email"],
    ["maxlength", `This field cant contain more than ${error?.['maxlength']?.requiredLength} characters`],
    ["minlength", `This field must contain more than ${error?.['minlength']?.requiredLength} characters`],
    ["match", 'Password and confirm password must match'],
    ["digits", 'This field must contain at least one digit'],
  ]);

  if (!errorsMap.has(errorKey)) {
    return '';
  }

  return errorsMap.get(errorKey) || '';

};
