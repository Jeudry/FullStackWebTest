import {Component, EventEmitter, Input, Optional, Output, Self} from '@angular/core';
import {getFormError} from "@core/helpers/get-form-error";
import {ControlContainer, ControlValueAccessor, FormControl, FormGroupDirective, NgControl} from "@angular/forms";

export const NOOP_VALUE_ACCESSOR: ControlValueAccessor = {
  writeValue(): void {
  },
  registerOnChange(): void {
  },
  registerOnTouched(): void {
  },
};


@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrl: './input.component.sass',
  viewProviders: [
    {
      provide: ControlContainer,
      useExisting: FormGroupDirective,
    },
  ]
})
export class InputComponent {
  @Input() type: string = '';
  @Input() placeholder: string = '';
  @Input() formControl: FormControl = new FormControl();
  @Input() disabled: boolean = false;
  @Input() value: string = '';
  @Input() onInputEmit: boolean = false;
  @Output() onInput = new EventEmitter<string>();

  constructor(@Self() @Optional() public ngControl: NgControl) {
    if (this.ngControl) {
      this.ngControl.valueAccessor = NOOP_VALUE_ACCESSOR;
    }
  }

  get errorMessage(): string {
    return getFormError(this.ngControl.errors!);
  }

  get isTouched() {
    return this.ngControl.touched;
  }

  onInputFunc() {
    if (this.onInputEmit) {
      this.onInput.emit(this.formControl.value);
    }
  }

  clear() {
    this.ngControl.control?.reset();
  }

}
