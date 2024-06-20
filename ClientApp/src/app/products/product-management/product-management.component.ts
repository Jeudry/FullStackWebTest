import {Component, Inject} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ProductsService} from "@products/products.service";
import {ProductDto} from "@core/interfaces/ProductDto";
import Swal from "sweetalert2";
import {InputType} from "@public/components/input/input.component";

@Component({
  selector: 'app-product-management',
  templateUrl: './product-management.component.html',
  styleUrl: './product-management.component.sass'
})
export class ProductManagementComponent {
  title: string = '';
  productId?: string;
  textArea: InputType = InputType.TextArea;

  managementForm!: FormGroup;
  name: FormControl = new FormControl(
    '',
    [Validators.required, Validators.minLength(3), Validators.maxLength(256)]
  );
  description: FormControl = new FormControl(
    '',
    [Validators.minLength(8), Validators.maxLength(1024)]
  );
  price: FormControl = new FormControl(
    null,
    [Validators.required]
  );
  stock: FormControl = new FormControl(
    null,
    [Validators.required]
  );

  constructor(
    private readonly fb: FormBuilder,
    private readonly route: ActivatedRoute,
    readonly dialogRef: MatDialogRef<ProductManagementComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private readonly productsService: ProductsService
  ) {
    this.initializeForm();
  }

  onClose() {
    this.dialogRef.close();
  }

  initializeForm() {
    this.managementForm = this.fb.group({
      name: this.name,
      description: this.description,
      price: this.price,
      stock: this.stock
    });

    this.productId = this._data.id;
    if (this.productId) {
      this.title = 'Edit Product';
      this.getProduct(this.productId);
    } else {
      this.title = 'Create Product';
    }
  }

  getProduct(productId: string) {
    this.productsService.getProduct(productId).subscribe({
      next: (product) => {
        this.managementForm.patchValue(product);
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  onSubmit(formValue: any) {
    const value = {...formValue}

    const productDto: ProductDto = {
      name: value.name,
      description: value.description,
      price: value.price,
      stock: value.stock
    }

    if (this.productId) {
      this.updateProduct(productDto);
    } else {
      this.createProduct(productDto);
    }
  }

  createProduct(productDto: ProductDto) {
    this.productsService.createProduct(productDto).subscribe({
      next: () => {
        Swal.fire({
          icon: 'success',
          title: 'Product created',
          showConfirmButton: false,
          timer: 1500
        }).then(() => {
          this.dialogRef.close(true);
        });
      }, error: (err) => {
        console.error(err);
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        }).then(() => {

        });
      }
    })
  }

  updateProduct(productDto: ProductDto) {
    this.productsService.updateProduct(this.productId!, productDto).subscribe({
      next: () => {
        Swal.fire({
          icon: 'success',
          title: 'Product updated',
          showConfirmButton: false,
          timer: 1500
        }).then(() => {
          this.dialogRef.close(true);
        });
      }, error: (err) => {
        console.error(err);
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        }).then(() => {

        });
      }
    })
  }

}
