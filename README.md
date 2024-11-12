<h2>Setup</h2>
<p dir="auto">Requirements:</p>
<ul dir="auto">
<li>Unity 6000.0.26f1 (other versions are not tested)</li>
</ul>

<p dir="auto">Steps:</p>
<ul dir="auto">
<li>git clone <a href="https://github.com/mofr/Diablerie.git">https://github.com/MarcherGA/ProductsDisplay.git</a></li>
<li>Run Unity Editor and open ProductsDisplay folder as a project</li>
<li>In Assets folder open <code>Scenes/StoreScene.scene</code> file</li>
<li>Modify <code>config.json</code> for your own configuration</li>
<li>Press <code>Play</code></li>
</ul>

<p dir="auto">Build (WebGL):</p>
<ul dir="auto">
<li>Create Build folder In Asset Folder</li>
<li>In Unity Editor, Select <code>File -> Build Profiles</code></li>
<li>In Build Profiles window, Select <code>Web</code> from Platforms</li>
<li>Press <code>Build And Run</code> and select Build folder</li>
<li>The build will be inside Build folder, and will run automatically</li>
</ul>

<h2>Code Design</h2>
<p dir="auto">Noteable Scripts:</p>
<ul dir="auto">
<li><code>ProductManager</code> - Creates and Manages the display of products, getting products data from dedicated service</li>
<li><code>ProductService</code> - Fetches products data from API</li>
<li><code>ProductFocus</code> - Manages camera transitions, zooming in and out on products</li>
<li><code>Product</code> - Represents Product in the scene, including properties for product information, and refrences to all product UI components</li>
<li><code>ProductTag</code> - Represents ProductTag in the scene, designated object for displaying and modifiying product information</li>
</ul>


<h2>Attributions</h2>
<p>"Check mark" icon - https://www.flaticon.com/free-icon/check-mark_5291032?term=check&page=1&position=10&origin=search&related_id=5291032v</p>
<p>"Close" icon - https://www.flaticon.com/free-icon/close_2976286?term=x&page=1&position=11&origin=search&related_id=2976286</p>
<p>"Edit" icon - https://www.flaticon.com/free-icon/edit_1159633?term=edit&page=1&position=2&origin=search&related_id=1159633</p>
<p>"Back Arrow" icon - https://iconscout.com/3d-illustration/back-arrow-8377696</p>
<p>"IKEA FJÄLLBO WALL SHELF" model - https://sketchfab.com/3d-models/ikea-fjallbo-wall-shelf-43479fde657649ddaf02d2db738407ee</p>
<p>"Question 3D icon" model - https://sketchfab.com/3d-models/question-3d-icon-ba8c685715a849fab6f289a2469d1567</p>
<p>"Modern Brick Wall 1" texture - https://freepbr.com/product/modern-brick1/</p>
<p>"Simple Helvetica" package - https://assetstore.unity.com/packages/tools/gui/simple-helvetica-2925</p>
