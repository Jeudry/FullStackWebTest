import {HttpErrorResponse, HttpInterceptorFn} from "@angular/common/http";
import {catchError, throwError} from "rxjs";

export const ErrorHandlerService: HttpInterceptorFn = (req, next) => {
  return next(req)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = handleError(error);
        return throwError(() => new Error(errorMessage));
      })
    )
};


const handleError = (error: HttpErrorResponse): string => {
  console.error(error)
  if (error.status === 500) {
    return handleInternalServer(error);
  }
  if (error.status === 404) {
    return handleNotFound(error);
  } else if (error.status === 405) {
    return handleStatus405(error);
  } else if (error.status === 400) {
    return handleBadRequest(error);
  } else if (error.status === 401) {
    return handleUnauthorized(error);
  } else if (error.status === 403) {
    return handleForbidden(error);
  } else {
    return error.message;
  }
}

const handleStatus405 = (error: HttpErrorResponse) => {
  return error.error.message;

}

const handleInternalServer = (error: HttpErrorResponse): string => {
  return error.error.error;
}

const handleForbidden = (error: HttpErrorResponse) => {
  return error.error.detail;
}

const handleNotFound = (error: HttpErrorResponse): string => {
  return error.error.detail;
}

const handleUnauthorized = (error: HttpErrorResponse) => {
  return error.error.message ? error.error.message : error.message;
}

const handleBadRequest = (error: HttpErrorResponse): string => {
  if (!error.error.error) {
    let message = '';
    if (error.error.errors) {
      const values = Object.values(error.error.errors);
      values.map((m: any) => {
        message += m + ' <br> ';
      });
      return message.slice(0, -6);
    } else {
      return "";
    }
  } else {
    return error.message;
  }
}

