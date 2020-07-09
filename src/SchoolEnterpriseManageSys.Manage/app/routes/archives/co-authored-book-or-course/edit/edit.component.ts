import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-co-authored-book-or-course-edit',
  templateUrl: './edit.component.html',
})
export class ArchivesCoAuthoredBookOrCourseEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
