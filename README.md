# MindBodySoul
<h2> Description </h2>
This project is a platform for sharing information about physical and spiritual well-being. The content is organized into categories and subcategories, with tags assigned to articles to simplify navigation. Registered users can read articles and leave comments, but only administrators have the ability to modify or manage the articles.

The backend is built with ASP.NET Core.

<h3>Used Libraries</h3>
<ul>
  <li>ng-select2-component</li>
  <li>cloudinary-core</li>
  <li>fortawesome</li>
  <li>ngx-editor</li>
</ul>

<h3>Some images</h3>
<img src="https://github.com/user-attachments/assets/30e66405-96ee-4496-93dd-fbca7392b65c"/>
<img src="https://github.com/user-attachments/assets/dc4846f7-131a-41ff-a1bc-29726eaa1ce0"/>
<img src="https://github.com/user-attachments/assets/2c6d906c-b82a-48c9-b548-876e627ddb13"/>
<img src="https://github.com/user-attachments/assets/4c1709b3-a863-4227-a302-a6b2dcac3b99"/>
<img src="https://github.com/user-attachments/assets/55286f27-4803-4a8d-8822-fc2921c0753c"/>
<img src="https://github.com/user-attachments/assets/f111330f-4aba-4ddc-8497-e7e23acd367b"/>

## Security Notes

Current status: 15 known vulnerabilities (11 moderate, 4 low)

Main issues:
- `@cloudinary/angular` uses Angular 10 internally (indirect dependency)
- `@fortawesome/angular-fontawesome` requires Angular 18 (we use 19)
- Build tool dependencies with moderate issues

These are acceptable for a portfolio project and will be resolved when:
- Library maintainers update their packages
- Angular releases compatibility patches

To check current security status: `npm audit`

