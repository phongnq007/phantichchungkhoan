import React from "react";

export interface BuyingRangeModel {
    exchange: string;
    symbol: string;
    companyName: string;
    price: number;
    buyPriceFrom: number;
    buyPriceTo: number;
    buyingStatus: string;
    }

interface     BuyingRangeItemProps {
    data: BuyingRangeModel
}

const BuyingRangeItem = (props: BuyingRangeItemProps) => {

    return (
        <tr>
                <td>
                    {props.data.exchange}
                </td>
                <td title="@item.CompanyName" data-toggle="tooltip" data-placement="right">
                    {props.data.symbol}
                </td>
                <td>
                {props.data.price}
                </td>
                <td>
                {props.data.buyPriceFrom}
                </td>
                <td>
                {props.data.buyPriceTo}
                </td>
                <td>
                {props.data.buyPriceFrom <= props.data.price && props.data.price <= props.data.buyPriceTo &&
                <span className="text-danger font-weight-bold">BUY</span> }

{props.data.price <= props.data.buyPriceFrom &&
                <span className="text-danger font-weight-bold">UNDER BUY</span> }
                </td>
                <td>
                    {/* <a asp-page="EditBuyingRange" asp-route-exchange="@item.Exchange" asp-route-symbol="@item.Symbol">Edit</a>
                    
                    <form method="post" asp-page-handler="Delete" onclick="return confirm('Are you sure you want to delete this?')">
                        <input type="hidden" name="exchange" value="@item.Exchange" />
                        <input type="hidden" name="symbol" value="@item.Symbol" />
                        <button type="submit" className="btn btn-link" style="padding: unset !important">Delete</button>
                    </form> */}
                </td>
            </tr>
    );
};

export default BuyingRangeItem;