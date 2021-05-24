import axios from 'axios';

const baseUrl = 'http://localhost:31105/api/';

export class ApiService {

    async GetListOfData<T>(url: string) {
        const result = await axios.get<T[]>(baseUrl + url)
        .then(response => {
            return response.data;
        });

        return result;
    }

}

