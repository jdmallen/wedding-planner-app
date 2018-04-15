import React from "react";

const Table = props => (
	<table>
		<thead>
			<tr>
				<th>ID</th>
				<th>Name</th>
				<th>Is Awesome?</th>
				<th>Date Created</th>
			</tr>
		</thead>
		<tbody>
			{
				props.list.map(row => (
					<tr key={row.id}>
						<td>{row.id}</td>
						<td>{row.name}</td>
						<td>{row.isAwesome ? "Yep!" : "Nope."}</td>
						<td>{new Date(row.createdAt * 1000).toISOString()}</td>
					</tr>
				))
			}
		</tbody>
	</table>
);
export default Table;
