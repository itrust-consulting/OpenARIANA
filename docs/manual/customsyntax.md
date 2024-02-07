# itrust syntax for Excel worksheets

itrust defines a syntax to allow for inline style specification. 

| Tag | Word style name | Description |
|------|----------------|--------------|
|*§B1 | Bullet L1 | Bullet point list, item on level 1. |
|*§B2 | Bullet L2 | Bullet point list, item on level 2. |
|*§B3 | Bullet L3 | Bullet point list, item on level 3. |
|*§B4 | Bullet L4 | Bullet point list, item on level 4. |
|*§SB1|	SP-BulletL1	|Bullet point list, item on level 1 inside SP (i.e. highlighted)|
|*§SB2|	SP-BulletL2	|Bullet point list, item on level 2 inside SP (i.e. highlighted)|
|*§SB3|	SP-BulletL3	|Bullet point list, item on level 3 inside SP (i.e. highlighted)|
|*§SB4|	SP-BulletL4	|Bullet point list, item on level 4 inside SP (i.e. highlighted)|
| *§E1 | Enumeration L1 | Text that should appear in the level 1 of an enumerated list. |
| *§E2 | Enumeration L2 | Text that should appear in the level 2 of an enumerated list. |
| *§E3 | Enumeration L3 | Text that should appear in the level 3 of an enumerated list. |
| *§E4 | Enumeration L4 | Text that should appear in the level 4 of an enumerated list. |
|*§EL	|End List	|Marks the end of a list. |
|*§SE1|	SP-EnumL1	|Text that should appear in the level 1 of an enumerated list |
|*§SE2|	SP- EnumL2	|Text that should appear in the level 2 of an enumerated list.|
|*§SE3|	SP- EnumL3	|Text that should appear in the level 3 of an enumerated list.|
|*§SE4|	SP- EnumL4	|Text that should appear in the level 4 of an enumerated list.|
|*§SEL|	End List	|Marks the end of a list.|
|*§C |  Caption |  Caption style for lists and images. |
|*§OB | SP-OtherInfoBullet |  Text style for inserting additional information to the text.  |
|*§QE | SP-Quote | Text style for adding quotes to text. |
|*§HI | Hidden |  Text not shown when printing the document  |

The following tags are defined by both a beginning tag - displayed in the table below - and an ending tag - the tag displayed below appended with "Ed". These tags define only sections of the text as the described styles.

For instance, to mark a section of the text in the SP-Bold style, the text should be between the tags *&B1 and *&B1Ed.

| Start Tag | End Tag | Word style name | Description |
|------|-----|----------------|--------------|
| *&B1 | *&B1Ed| SP-Block | Defines a text inside a block in the text. |
| *&D1 | *&D1Ed| SP-Bold | Defines a section of the text that should be in bold font. |
| *&W1 | *&W1Ed| SP-Specific1 |  |
| *&W2 | *&W2Ed| SP-Specific2 |  |
| *&HI | *&HIEd| Hidden Char | A sequence of characters that should not appear in the printed version of the document. |