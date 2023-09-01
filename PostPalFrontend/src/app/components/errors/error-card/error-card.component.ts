import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BackendException } from '../../../exceptions/project-exception';

@Component({
  selector: 'pal-error-card',
  templateUrl: './error-card.component.html',
  styleUrls: ['./error-card.component.scss']
})
export class ErrorCardComponent implements OnInit {
	@Input() error!: BackendException;
	@Output() close: EventEmitter<void> = new EventEmitter();

  constructor() { }

  public ngOnInit(): void {
	}

	public onClose(): void {
		this.close.emit();
	}
}
