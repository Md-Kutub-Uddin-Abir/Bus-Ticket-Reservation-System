import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-seat-view',
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './seat-view.html',
  styleUrls: ['./seat-view.css']
})
export class SeatView implements OnInit {
  busScheduleId!: number;
  seats: any[] = [];
  selectedSeats: number[] = [];
  passengerName = '';
  passengerMobile = '';

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    this.busScheduleId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadSeats();
  }

  loadSeats() {
    const url = `http://localhost:5232/api/ticket/${this.busScheduleId}`;
    this.http.get<any[]>(url).subscribe({
      next: (res) => {
        this.seats = res;
      },
      error: (err) => {
        console.error(err);
        alert('Failed to load seats.');
      },
    });
  }

  toggleSeat(seat: any) {
  if (seat.status === 1) return; // already booked

  const seatNo = seat.seatNo; 

  if (this.selectedSeats.includes(seatNo)) {
    this.selectedSeats = this.selectedSeats.filter((n) => n !== seatNo);
  } else {
    this.selectedSeats.push(seatNo);
  }
  }


  isSelected(seat: any): boolean {
    return this.selectedSeats.includes(seat.seatNo);
  }


  bookSeats() {
    if (this.selectedSeats.length === 0) {
      alert('Please select at least one seat.');
      return;
    }
    if (!this.passengerName || !this.passengerMobile) {
      alert('Please enter passenger name and mobile.');
      return;
    }

    const body = {
      busScheduleId: this.busScheduleId,
      seatNumbers: this.selectedSeats,
      passengerName: this.passengerName,
      passengerMobile: this.passengerMobile,
    };

    this.http.post('http://localhost:5232/api/ticket/book', body).subscribe({
      next: (res) => {
        alert('Seat(s) booked successfully!');
        this.selectedSeats = [];
        this.passengerName = '';
        this.passengerMobile = '';
        this.loadSeats();
      },
      error: (err) => {
        console.error(err);
        alert('Booking failed.');
      },
    });
  }
}
