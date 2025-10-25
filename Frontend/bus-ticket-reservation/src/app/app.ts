import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SearchBus } from './components/search-bus/search-bus';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,SearchBus],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('bus-ticket-reservation');
}
