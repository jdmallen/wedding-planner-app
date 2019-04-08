import React from "react";
import { CardDeck } from "reactstrap";
import RegistryCard from "./RegistryCard";
import bbbLogo from "../../Images/bed_bath_beyond_logo.png";
import amazonLogo from "../../Images/amazon_logo.png";
import styles from "./Registries.scss";

const Stores = function Stores()
{
	const amazon =
		(<RegistryCard
			logo={amazonLogo}
			name="Amazon.com"
			// eslint-disable-next-line max-len
			websiteUrl="https://smile.amazon.com"
			// eslint-disable-next-line max-len
			registryUrl="https://smile.amazon.com/wedding/kristen-merritt-jesse-mallen-geneva-july-2019/registry/3IA093643JPQR"
		/>);

	const bbb =
		(<RegistryCard
			logo={bbbLogo}
			name="Bed Bath & Beyond"
			// eslint-disable-next-line max-len
			websiteUrl="https://www.bedbathandbeyond.com"
			// eslint-disable-next-line max-len
			registryUrl="https://www.bedbathandbeyond.com/store/giftregistry/viewregistryguest/547241477?eventType=Wedding"
		/>);

	return (
		<div>
			<h1>Gift Registries</h1>
			<p>
				{"We are registered at the below stores. We tried our best to ensure "}
				{"gifts were not duplicated across stores, so each one should be "}
				{"different!"}
			</p>
			<CardDeck>
				{amazon}
				{bbb}
			</CardDeck>
			<p className={styles.troubleText}>
				<em>
					If any hotel reports that they&apos;re out of rooms,
					please <a href="mailto:us@kristenandjesse.com">let us know</a>!
				</em>
			</p>
		</div>
	);
};

export default Stores;
