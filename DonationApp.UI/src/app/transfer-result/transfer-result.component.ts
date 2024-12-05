import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-transfer-result',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './transfer-result.component.html',
  styleUrl: './transfer-result.component.css',
})
export class TransferResultComponent implements OnInit {
  amount: number = 0;
  note: string = '';
  success: boolean = false;

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params: any) => {
      this.amount = params['amount'];
      this.note = params['note'];
      this.success = params['success'] === 'true';

      console.log(this.success);
    });
  }
}
