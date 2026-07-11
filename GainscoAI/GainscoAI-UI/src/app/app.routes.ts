import { Routes } from '@angular/router';

import { Home } from './pages/home/home';
import { Upload } from './pages/upload/upload';
import { Chat } from './pages/chat/chat';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'upload', component: Upload },
  { path: 'chat', component: Chat }
];