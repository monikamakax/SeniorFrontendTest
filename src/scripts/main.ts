import { setUpToggleEventListeners } from "./components/toggle";
import { initCart } from "./components/cart";
import { setUpFilterListener } from "./components/filter";

setUpToggleEventListeners();
setUpFilterListener();
initCart();

// Todo: Implement product filtering logic towards the /api/products/filter endpoint
