import { Component } from '@angular/core';
import { Actor } from '../models/actor';


@Component({
  selector: 'app-actor-form',
  templateUrl: './actor-form.component.html',
  standalone: false,
})
export class ActorFormComponent {
  skills = ['Method Acting', 'Singing', 'Dancing', 'Swordfighting'];
  model = new Actor(18, 'Tom Cruise', this.skills[3], 'CW Productions');

  submitted = false;
  onSubmit() {
    this.submitted = true;
  }
}
