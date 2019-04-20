import React from "react";
import { Table } from "reactstrap";
import itineraryItems from "./_itineraryItems";
// eslint-disable-next-line no-unused-vars
import styles from "./Itinerary.scss";

const Itinerary = () => (
	<div>
		<h1>Wedding Itinerary</h1>
		<p>
			{"Our special day in a nutshell!"}
		</p>
		<p>
			{"Note that events marked with an \""}
			<strong>*</strong>
			{"\" are approximately timed and may occur "}
			{"earlier or later than the time listed."}
		</p>
		<Table>
			<tbody>
				{
					itineraryItems.map(item => (
						<tr key={item.index}>
							<th>{item.time}</th>
							<td>{item.event}</td>
						</tr>
					))
				}
			</tbody>
		</Table>
	</div>
);

export default Itinerary;
