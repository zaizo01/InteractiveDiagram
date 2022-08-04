import axios from 'axios';
import * as names from '../names';

const getInstitutionStructure = () => {
    let url = `${names.API_ENDPOINT_INSTITUTION_STRUCTURE}`;
    return new Promise((resolve, reject) => {
        axios.get(url, {headers: {
            "Content-Type": "application/json"}
            })
            .then((response) => {
                resolve(response);
            })
            .catch((error) => {
                reject(error)
            })
    });
}

const getInstitutionRecords = (institutionId) => {
    let url = `${names.API_ENDPOINT_INSTITUTION_RECORDS}${institutionId}`;
    return new Promise((resolve, reject) => {
        axios.get(url, {headers: {
            "Content-Type": "application/json"}
            })
            .then((response) => {
                resolve(response);
            })
            .catch((error) => {
                reject(error);
            })
    })
}

const getInstitutionRecordById = (institutionUuid) => {
    let url = `${names.API_ENDPOINT_INSTITUTION_STRUCTURE}/${institutionUuid}`;
    return new Promise((resolve, reject) => {
        axios.get(url, {headers: {
            "Content-Type": "application/json"}
            })
            .then((response) => {
                resolve(response);
            })
            .catch((error) => {
                reject(error);
            })
    });
}

const postInstitutionStructure = (Record) => {
    let url = `${names.API_ENDPOINT_INSTITUTION_STRUCTURE}`;
    return new Promise((resolve, reject) => {
        axios.post(url, Record, {headers: {
            "Content-Type": "application/json"}
            })
            .then((response) => {
                resolve(response);
            })
            .catch((error) => {
                reject(error)
            })
    });
}

const putInstitutionStructure = (Record) => {
    let url = `${names.API_ENDPOINT_INSTITUTION_STRUCTURE}`;
    return new Promise((resolve, reject) => {
        axios.put(url, Record, {headers: {
            "Content-Type": "application/json"}
            })
            .then((response) => {
                resolve(response);
            })
            .catch((error) => {
                reject(error);
            })
    });
}

const deleteInstitutionStructure = (institutionId, userEmail) => {
    let url = `${names.API_ENDPOINT_INSTITUTION_STRUCTURE}/${institutionId}/${userEmail}`;
    return new Promise((resolve, reject) => {
        axios.delete(url, {headers: {
            "Content-Type": "application/json"}
            })
            .then((response) => {
                resolve(response);
            })
            .catch((error) => {
                reject(error);
            })
    });
}

const getInstitutionStructurePagination = (params) => {
    let url = `${names.API_ENDPOINT_INSTITUTION_STRUCTURE}`;
    return new Promise((resolve, reject) => {
        axios.get(url, { params }, {headers: {
            "Content-Type": "application/json"}
            })
            .then((response) => {
                resolve(response);
            })
            .catch((error) => {
                reject(error);
            })
    })
}

const getDiagramStructureRecords = (institutionId) => {
    let url = `${names.API_ENDPOINT_INSTITUTION_DIAGRAM_RECORDS}/${institutionId}`;
    return new Promise((resolve, reject) => {
        axios.get(url, {headers: {
            "Content-Type": "application/json"}
            })
            .then((response) => {
                resolve(response);
            })
            .catch((error) => {
                reject(error);
            })
    })
}

export {
    getInstitutionStructure,
    getInstitutionRecordById,
    postInstitutionStructure,
    putInstitutionStructure,
    deleteInstitutionStructure,
    getInstitutionStructurePagination,
    getDiagramStructureRecords,
    getInstitutionRecords
};