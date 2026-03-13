import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { BookService } from './services/book.service';
import { Book } from './models/book.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  books: Book[] = [];
  book: Book = { id: 0, title: '', author: '', isbn: '', publicationDate: '' };
  isEdit = false;

  constructor(private bookService: BookService) {}

  ngOnInit() {
    this.loadBooks();
  }

  loadBooks() {
    this.bookService.getBooks().subscribe(res => this.books = res);
  }

  saveBook() {
    if (this.isEdit) {
      this.bookService.updateBook(this.book.id, this.book).subscribe(() => {
        this.clearForm();
        this.loadBooks();
      });
    } else {
      this.bookService.addBook(this.book).subscribe(() => {
        this.clearForm();
        this.loadBooks();
      });
    }
  }

  editBook(selectedBook: Book) {
    this.book = { ...selectedBook };
    this.isEdit = true;
  }

  deleteBook(id: number) {
    if (confirm('Delete this book?')) {
      this.bookService.deleteBook(id).subscribe(() => this.loadBooks());
    }
  }

  clearForm() {
    this.book = { id: 0, title: '', author: '', isbn: '', publicationDate: '' };
    this.isEdit = false;
  }
}