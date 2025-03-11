export interface Dinosaur {
    name: string;
    species: string;
    isCarnivorous: boolean;
    isSick: boolean;
    lastFed: string; // ISO date string
}

export interface ZoneRequest {
    name: string;
    isOpen: boolean;
}

export interface ZoneToggleRequest {
    zoneName: string;
}

export interface AddDinosaurRequest {
    zoneName: string;
    dinosaur: Dinosaur;
}

export interface MoveDinosaurRequest {
    fromZoneName: string;
    toZoneName: string;
    dinosaurName: string;
}

export interface SpeciesCoexistRequest {
    species1: string;
    species2: string;
}

export class JurassicParkClient {
    private readonly baseUrl: string;

    constructor(baseUrl: string = '/api') {
        this.baseUrl = baseUrl;
    }

    private async fetchJson<T>(url: string, method: string, body?: any): Promise<T> {
        const response = await fetch(url, {
            method,
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            credentials: 'include',
            mode: 'cors',
            body: body ? JSON.stringify(body) : undefined
        });

        if (!response.ok) {
            const errorText = await response.text();
            console.error(`API error (${response.status}): ${errorText}`);
            throw new Error(`API error (${response.status}): ${errorText}`);
        }

        return response.json();
    }

    async addZone(request: ZoneRequest): Promise<string> {
        return this.fetchJson<string>(`${this.baseUrl}/Park/AddZone`, 'POST', request);
    }

    async addDinosaurToZone(request: AddDinosaurRequest): Promise<string> {
        return this.fetchJson<string>(`${this.baseUrl}/Park/AddDinosaurToZone`, 'POST', request);
    }

    async moveDinosaur(request: MoveDinosaurRequest): Promise<string> {
        return this.fetchJson<string>(`${this.baseUrl}/Park/MoveDinosaur`, 'POST', request);
    }

    async toggleZone(request: ZoneToggleRequest): Promise<string> {
        return this.fetchJson<string>(`${this.baseUrl}/Park/ToggleZone`, 'POST', request);
    }

    async canSpeciesCoexist(request: SpeciesCoexistRequest): Promise<boolean> {
        return this.fetchJson<boolean>(`${this.baseUrl}/Park/CanSpeciesCoexist`, 'POST', request);
    }

    async getDinosaursInZone(zoneName: string): Promise<Dinosaur[]> {
        return this.fetchJson<Dinosaur[]>(`${this.baseUrl}/Park/GetDinosaursInZone`, 'POST', { name: zoneName });
    }
    
    async getAllZones(): Promise<{ name: string; isOpen: boolean; dinosaurs: Dinosaur[] }[]> {
        return this.fetchJson<{ name: string; isOpen: boolean; dinosaurs: Dinosaur[] }[]>(`${this.baseUrl}/Park/GetAllZones`, 'GET');
    }
}