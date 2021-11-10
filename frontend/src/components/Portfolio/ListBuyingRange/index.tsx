import React, { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import httpClient from "../../../utils/httpClient";
import BuyingRangeItem, { BuyingRangeModel } from "../partials/buyingRangeItem";

interface ListBuyingRangeState {
    data: Array<BuyingRangeModel>;
    message: string;
}

const getData = async (setBuyingRangeData: React.Dispatch<React.SetStateAction<ListBuyingRangeState>>) => {

    try {
        const response = await httpClient.get("/portfolio/get-buying-range");

        setBuyingRangeData({ data: response.data, message: "" });
    } catch (error) {
        setBuyingRangeData({ data: [], message: error.message });
    }
};

const ListBuyingRange = () => {
    const [buyingRangeData, setBuyingRangeData] = useState<ListBuyingRangeState>({
        data: [], message: ""
    });

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await httpClient.get("/portfolio/get-buying-range");
        
                setBuyingRangeData({ data: response.data, message: "" });
            } catch (error) {
                setBuyingRangeData({ data: [], message: error.message });
            }
        };
        
        fetchData();
    }, [setBuyingRangeData]);
    //getData(setBuyingRangeData);

    return <>
        <h1>Portfolios</h1>

        <p>
            <NavLink to="/create-buying-range">Create New</NavLink>
        </p>
        <table className="table">
            <thead>
                <tr>
                    <th className="align-text-top">
                        {buyingRangeData.data && buyingRangeData.data.length > 0 &&
                            <span className="text-danger font-weight-bold">{buyingRangeData.data.length}</span>}
                    </th>
                    <th className="align-text-top">
                        Symbol
            </th>
                    <th className="align-text-top">
                        Price
            </th>
                    <th className="align-text-top">
                        BuyPrice From
            </th>
                    <th className="align-text-top">
                        BuyPrice To
            </th>
                    <th className="align-text-top">
                        Buying Status
            </th>
                    <th className="align-text-top"></th>
                </tr>
            </thead>
            <tbody>
                {buyingRangeData.data.length > 0 && buyingRangeData.data.map(item =>
                    <BuyingRangeItem key="" data={item} />)}
            </tbody>
        </table>
    </>;
};

export default ListBuyingRange;