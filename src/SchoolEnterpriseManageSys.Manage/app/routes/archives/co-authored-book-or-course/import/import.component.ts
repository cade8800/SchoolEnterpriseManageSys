import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-co-authored-book-or-course-import',
  templateUrl: './import.component.html',
})
export class ArchivesCoAuthoredBookOrCourseImportComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
