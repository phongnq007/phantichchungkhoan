import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import httpClient from "../../../utils/httpClient";

export interface BuyingRangeModel {
    symbol: string;
    buyPriceFrom: number;
    buyPriceTo: number;
}

const CreateBuyingRange = () => {
    const [buyingRangeModel, setBuyingRangeModel] = useState<BuyingRangeModel>({
        buyPriceFrom: 0, buyPriceTo: 0, symbol: ""
    });

    const [errorMessage, setErrorMessage] = useState("");

    const symbolChanged = (e: any) => {
        let nextState = { ...buyingRangeModel };
        nextState.symbol = e.target.value;
        setBuyingRangeModel(nextState);
    };

    const buyPriceFromChanged = (e: any) => {
        let nextState = { ...buyingRangeModel };
        try {
            nextState.buyPriceFrom = Number.parseFloat(e.target.value);
        } catch (error) {
            setErrorMessage("BuyPriceFrom is invalid!");
            return;
        }
        setBuyingRangeModel(nextState);
    };

    const buyPriceToChanged = (e: any) => {
        let nextState = { ...buyingRangeModel };
        try {
            nextState.buyPriceTo = Number.parseFloat(e.target.value);
        } catch (error) {
            setErrorMessage("BuyPriceTo is invalid!");
            return;
        }
        setBuyingRangeModel(nextState);
    };

    const submit = (e: any) => {
        return async () => {
            try {
                var response = await httpClient.post("/portfolio/add-buying-range", buyingRangeModel);
                setErrorMessage("Buying range is created successfully!");
            } catch (error) {
                setErrorMessage(error.message);
            }
        };
    };

    return <>
        <h4>Create buying range</h4>
        <hr />
        <div className="row">
            <div className="col-md-4">
                <form onSubmit={submit}>
                    <div asp-validation-summary="ModelOnly" className="text-danger">
                        {errorMessage}
                    </div>
                    <div className="form-group">
                        <label asp-for="BuyingRange_Symbol" className="control-label">Symbol</label>
                        <input id="BuyingRange_Symbol" value={buyingRangeModel.symbol} onChange={symbolChanged} className="form-control" />
                    </div>
                    <div className="form-group">
                        <label asp-for="BuyingRange_BuyPriceFrom" className="control-label"></label>
                        <input id="BuyingRange_BuyPriceFrom" value={buyingRangeModel.buyPriceFrom} onChange={buyPriceFromChanged} className="form-control" />
                    </div>
                    <div className="form-group">
                        <label asp-for="BuyingRange_BuyPriceTo" className="control-label"></label>
                        <input id="BuyingRange_BuyPriceTo" value={buyingRangeModel.buyPriceTo} onChange={buyPriceToChanged} className="form-control" />
                    </div>
                    <div className="form-group">
                        <input type="submit" value="Create" className="btn btn-primary" />
                    </div>
                </form>
            </div>
        </div>

        <div className="text-center">
            <NavLink to="/buyingrange">Back to List</NavLink>
        </div>
    </>
};

export default CreateBuyingRange;