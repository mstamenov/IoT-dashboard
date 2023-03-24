import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  telemetry?: Telemetry;
  title = 'iot-dashboard';
  data?: HourlyTelemetryVO[];
  options?: any;
  ld?: any;

  async ngOnInit() {

    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');


    this.http.get<Telemetry[]>('/Telemetry/Current').subscribe(data => {
      this.telemetry = data[0];
    });

    this.http.get<HourlyTelemetryVO[]>('/Telemetry/LastDay').subscribe(data => {
      data.forEach(x => x.date = new Date(x.date));
      this.data = data;
      this.ld = {
        labels: data.map(x => `${x.date.toDateString()} / ${x.hour}` ),
        datasets: [
            {
                label: 'First Dataset',
                data: this.data.map(x => x.temperature),
                fill: false,
                borderColor: documentStyle.getPropertyValue('--blue-500'),
                tension: 0.4
            }
        ]
    }});

    this.options = {
      maintainAspectRatio: false,
      aspectRatio: 0.6,
      plugins: {
          legend: {
              labels: {
                  color: textColor
              }
          }
      },
      scales: {
          x: {
              ticks: {
                  color: textColorSecondary
              },
              grid: {
                  color: surfaceBorder,
                  drawBorder: false
              }
          },
          y: {
              ticks: {
                  color: textColorSecondary
              },
              grid: {
                  color: surfaceBorder,
                  drawBorder: false
              }
          }
      }
    }
  }

  constructor(private http: HttpClient) {

  }
}

interface Telemetry{
  createDate: Date;
  temperature: number;
  humidity: number;
  pressure: number;
  deviceId: string;
}

interface HourlyTelemetryVO{
  date: Date;
  hour: number;
  temperature: number;
  humidity: number;
  pressure: number;
  deviceId: string;
}
