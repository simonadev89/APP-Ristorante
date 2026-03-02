export interface Restaurant {
  id: number;
  name: string;
  address: string;
  city: string;
  description: string;
  totalSeats: number;
  reservations: any[]; // Puoi definire meglio questo tipo se conosci la struttura delle prenotazioni

}

export  interface ApiResponse<T> {
  success: boolean;
  message: string | null;
  data: T;
}