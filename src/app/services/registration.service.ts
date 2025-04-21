import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  static searchTrainer(term: string): any[] {
    return fetch(`https://buero.luebeckregatta.de/api/Trainer?search=${term}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json'
      }}).then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      }).catch(error => {
        console.error('There has been a problem with your fetch operation:', error);
        return [];
      }
    );
  }

  private readonly apiUrl = 'https://buero.luebeckregatta.de/api/Finish';
  private readonly apiKey = '37FD7F0F-EDA3-4DCA-983F-C8AED6AADF12';

  constructor() { }
}
