import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-search-form',
  imports: [ReactiveFormsModule],
  templateUrl: './search-form.html',
  styleUrl: './search-form.scss'
})
export class SearchForm implements OnInit {
  protected searchForm!: FormGroup;

  public ngOnInit(): void {
    this.searchForm = new FormGroup({
      searchQuery: new FormControl('', [Validators.required]),
    });
  }

  protected onSubmit() {
    console.log(this.searchForm.value);
  }

}
