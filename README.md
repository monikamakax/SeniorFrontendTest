## Context
You have a simple e-commerce app that:
- Fetches products from `ProductService.cs`
- Displays the products in a list

## Task
Your task is to design and implement a filter panel for the product list.
An unfinished API endpoint is available in `ProductController.cs` at:`POST /api/products/filter`  
Timebox: 1–2 hours

## Requirements
- Ability to filter products with lime flavour only.
- Ability to filter products that are beverages only.

## Deliverable
- Fork the repository and commit your solution.
- Invite gismya and corneliahaypp to your fork. (Settings > Collaborators)

## Technical interview
During the technical interview, we’ll review your changes together and discuss:  
  
- Your approach and implementation of the filter functionality  
- Ideas for improving and extending the existing codebase  
- Be prepared to explain your design decisions and share how you would enhance the overall structure, performance, or maintainability of the application.  

## Getting Started

1. Fork the repository
2. Create a code space
<img width="1405" height="753" alt="image" src="https://github.com/user-attachments/assets/a62aaae1-bb45-4a45-aa8d-504007d2fd78" />


### Running without debugging
1. Open a terminal in the codespace.
2. Install dependencies:
   ```bash
   npm i
   ```
3. Start the development server:
   ```bash
   npm run dev
   ```
4. Now the project should be running on http://localhost:5001

### Running without debugging
1. Click Run and debug  
<img width="412" height="416" alt="image" src="https://github.com/user-attachments/assets/25c1694a-07d1-406d-8dad-cf35e2744241" />  

2. Select debugger, choose C#  
<img width="788" height="261" alt="image" src="https://github.com/user-attachments/assets/1e5ac311-3517-44d1-a63e-6824a0a1c155" />  

3. Select startup project and CodeTest  
<img width="769" height="204" alt="image" src="https://github.com/user-attachments/assets/ec0d4e15-6f0b-438d-a3d1-383db69e5d28" />  

4. Start debugging (F5)  
<img width="375" height="119" alt="image" src="https://github.com/user-attachments/assets/bcda86c5-7109-420d-a5b5-bd2b469ff494" />  

5. Now the project should be running on http://localhost:5001  

You can then set breakpoints in the IDE to debug the C# code.
_Note that this option won't run the TS/Less compilation_



