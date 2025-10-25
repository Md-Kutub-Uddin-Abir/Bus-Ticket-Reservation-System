import { Routes } from '@angular/router';
import { SearchBus } from './components/search-bus/search-bus';
import { SeatView } from './components/seat-view/seat-view';

export const routes: Routes = [
  { path: '', component: SearchBus },
  { path: 'seats/:id', component: SeatView },
];
