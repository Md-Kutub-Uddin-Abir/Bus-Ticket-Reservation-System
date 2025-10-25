import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'app-search-bus',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './search-bus.html'
})
export class SearchBus {
  from = '';
  to = '';
  journeyDate = '';
  busSchedules: any[] = [];

  constructor(private http: HttpClient, private router: Router) {}

  searchBus() {
  if (!this.from || !this.to || !this.journeyDate) {
    alert('Please fill all fields');
    return;
  }

  const currentFrom = this.from;
  const currentTo = this.to;
  const currentDate = this.journeyDate;

  const url = `http://localhost:5232/api/search?from=${currentFrom}&to=${currentTo}&journeyDate=${currentDate}`;
  this.http.get<any[]>(url).subscribe({
    next: (res) => {
      if (res.length === 0) {
        this.busSchedules = [];
        alert('No buses found.');
      } else {
        
        this.busSchedules = res.map(bus => ({
          ...bus,
          from: currentFrom,
          to: currentTo,
          journeyDate: currentDate
        }));
      }
    },
    error: (err) => {
      console.error(err);
      this.busSchedules = [];
      alert('No buses found or server error.');
    },
  });
}

  setTrending(from: string, to: string) {
  this.from = from;
  this.to = to;
}


  viewSeats(busScheduleId: number) {
    this.router.navigate(['/seats', busScheduleId]);
  }
}
