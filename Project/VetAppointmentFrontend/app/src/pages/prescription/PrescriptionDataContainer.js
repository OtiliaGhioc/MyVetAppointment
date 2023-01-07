import { Button } from '@mui/material';
import Container from '@mui/material/Container';
import * as React from 'react';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from "react-router-dom";
import { API_ROOT } from '../../env';
import { getAccessToken, getRefreshToken, makeRequestWithJWT } from "../../util/JWTUtil";


const PrescriptionDataContainer = () => {

    const navigate = useNavigate();
    const [prescription, setPrescription] = useState();
    let { id } = useParams();
    //console.log(id);

    const fetchDataPrescription = async () => {
        let path = `${API_ROOT}/v1.0/Prescriptions`///${id}`
        const response = await makeRequestWithJWT(
            path, {
            method: 'GET',
            mode: 'cors'
        }, {
            accessToken: getAccessToken(),
            refreshToken: getRefreshToken()
        }
        )

        if (!response.ok) {
            navigate("/not-found");
            return;
        }

        const json_data = await response.json();
        //console.log(json_data);

        setPrescription(json_data);
    }

    async function cancelPrescription(prsID) {
        const res = await fetch(`${API_ROOT}/v1.0/Prescriptions/${prsID}`, {
            method: 'DELETE',
            mode: 'cors'
        });

        if (res.ok) {
            navigate("/me");
            return;
        }
    }

    function renderPrescriptions() {
        const prescriptionList = [];
        for(let i = 0; i < prescription.length; i++) {
            let drug = prescription[i].drugs;
            let descr = prescription[i].description;
            let id = prescription[i].prescriptionId;
            
            prescriptionList.push(<Container style={{ width: '100%', padding: '1rem', backgroundColor: "#8fc3e3" }}>
            <h3>Drugs: {drug}</h3>
            <h3>Description: {descr}</h3>
            <Button variant="contained" style={{ margin: '0 auto 0 1rem', border: '2px solid', color: 'red' }} onClick={() => cancelPrescription(id)}>Cancel</Button>
        </Container>);
        }
  
        return prescriptionList;
    }

    useEffect(() => {
        document.body.style.backgroundColor = '#ebf6fc';

        fetchDataPrescription();

    }, [navigate])

    if (prescription)
        return (
            <div>
                {
                    renderPrescriptions()
                }
            </div>
        )
}

export default PrescriptionDataContainer;