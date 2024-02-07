# OpenARIANA

OpenARIANA was developed to address the repetitive task of creating policies, particularly Information Security Management System (ISMS) policies. These documents often consist of standardized text that needs to be tailored to individual customers' requirements. By integrating closely with Microsoft Word, OpenARIANA streamlines the process of document creation and customization in professional settings. It offers a user-friendly interface that enhances productivity and reduces manual effort, making the adaptation of standardized policies to specific client needs both efficient and reliable.

## Proclaimer

itrust maintains a repository of ISMS standards like ISO 2700x in a structured format compatible with OpenARIANA. Users who wish to access these standards can contact us at [openariana@itrust.lu](mailto:openariana@itrust.lu). Please include evidence that you are eligible for the standard, such as a license for the requested standard. Upon verification, we will provide the structured standard free of charge.

## Getting Started 

### For End-Users

#### Requirements
- Operating System: Windows 10/11 (x64)
- Microsoft Office: Word/Excel

#### Installation

1. Download the installer from [here](https://github.com/itrust-consulting/OpenARIANA/releases/download/0.1.0/OpenARIANA.msi).
2. Run the installer with Administrator privileges.
3. Launch Microsoft Word. OpenARIANA will be available in the Ribbon menu.

**Assurance and Safety**: For security scanning results and to verify the integrity of the downloaded installer, check if it matches the SHA-256 checksum provided on [VirusTotal](https://www.virustotal.com/gui/file-analysis/MjI4N2JiOWE0NjkwNWJiYmYzNzc2ODhkYWI0OTZkYmU6MTcwNzE0NTM1MQ==)

### For Developers

#### Requirements for Developers

- Operating System: Windows 10/11
- Microsoft Office: Word/Excel
- Microsoft Visual Studio 2022 or newer (not Visual Studio Code)
- git

#### Build OpenARIANA

1. Clone the repository: 
2.	Open "OpenARIANA.sln" with Visual Studio 2022 (ensure both 'OpenARIANA' and 'OpenARIANASetup' projects are correctly loaded);
3.	Generate a Certificate for the ClickOnce manifests (see [instructions](https://learn.microsoft.com/en-us/visualstudio/ide/how-to-sign-application-and-deployment-manifests?view=vs-2022)).
    1.	Right click OpenARIANA in the Solution Explorer and go to ‘Properties’
    2.	Navigate to ‘Signing’
    3.	Select or create a certificate
    4.	Save changes
4.	Build ‘OpenARIANASetup’

## Usage

### User Manual

Access comprehensive guides for various features:

- [Quick Start Guide](./docs/manual/quickstart.md)
- [Profile Management & Tag Replacement](./docs/manual/profiles.md)

The user manual is also available as [PDF](./docs/manual/_pdf/5OC4_REP_CyFORT-OpenARIANA-UserManual_v1.0.pdf)

## Roadmap

*Details coming soon.*

## License

Copyright © itrust consulting. All rights reserved.

Licensed under the [GNU Affero General Public License (AGPL) v3.0](LICENSE).

## Contact

For more information about the project, contact us at [openariana@itrust.lu](mailto:openariana@itrust.lu).


## Changelog

### [0.1.0]

#### Added

- implemented tag replacement logic (ProfileManager)
- implemented writers, parsers, and processors to handle import and export
- implemented a basic processor that does not rely on itrust resources
- included Settings Menu to allow for user customization
    - Change tag pattern
    - Change logger verbosity
    - Change profile Directory
    - Manage selected profile
    - Open log file
- implemented a Logger and greatly improved overall error handling and traceability
- implemented custom exception handler
- implemented a File System Manager
- included readme and user manual

#### Changed

- restructured and refactored entire code base
- reworked UI design (Ribbon Menu and Task Pane)
- reworked import process
- reworked the initial import logic to use parser, writer, processor approach. Post-processing to handle inline itrust syntax remained untouched.
- updated readme and user manual

#### Removed

- removed all audit elements (Add recommendation, Generate Conclusion) due to immaturity of implementation and concept
- removed parsers, writers, and processors working on unfinished AST

#### Fixed

- Task pane toggling behaviour
