import axios from 'axios';
import * as names from '../names';

const getInstitutions = () => {
    let url = `${names.API_ENDPOINT_INSTITUTION}`; 
    return new Promise((resolve, reject) => {
        axios.get(url)
        .then((response) => {
            resolve(response);
        })
        .catch((error) => {
            reject(error)
        })
    });
}

const getInstitutionById = (departmentId) => {
    let url = `${names.API_ENDPOINT_INSTITUTION}/${departmentId}`;
    return new Promise((resolve, reject) => {
        axios.get(url)
        .then((response) => {
            resolve(response);
        })
        .catch((error) => {
            reject(error);
        })
    });
}

const postInstitution = (institutionId, name, referenceId) => {
    let url = `${names.API_ENDPOINT_INSTITUTION}`;
    let request = {
        institutionId: institutionId,
        name: name,
        referenceId: referenceId,
    }
    return new Promise((resolve, reject) => {
        axios.post(url, request)
        .then((response) => {
            resolve(response);
        })
        .catch((error) => {
            reject(error)
        })
    });
}

const putInstitution = (id, institutionId, name, referenceId) => {
    let url = `${names.API_ENDPOINT_INSTITUTION}/${id}`;
    let request = {
        id: id,
        institutionId: institutionId,
        name: name,
        referenceId: referenceId
    }
    return new Promise((resolve, reject) => {
        axios.put(url, request)
        .then((response) => {
            resolve(response);
        })
        .catch((error) => {
            reject(error);
        })
    });
}

const deleteInstitution = (departmentId, userEmail) => {
    let url = `${names.API_ENDPOINT_INSTITUTION}/${departmentId}/${userEmail}`;
    return new Promise((resolve, reject) => {
        axios.delete(url)
        .then((response) => {
            resolve(response);
        })
        .catch((error) => {
            reject(error);
        })
    });
}

const getInstitutionPagination = (params) => {
    let url = `${names.API_ENDPOINT_INSTITUTION}/users`;
    return new Promise((resolve, reject) => {
        axios.get(url, { params })
        .then((response) => {
            resolve(response);
        })
        .catch((error) => {
            reject(error);
        })
    })
}



export {
    getInstitutions, 
    getInstitutionById, 
    postInstitution, 
    putInstitution, 
    deleteInstitution, 
    getInstitutionPagination 
};