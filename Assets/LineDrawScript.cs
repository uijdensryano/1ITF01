using System;
using System.Text.RegularExpressions;
using UnityEngine;
using Mathos.Parser;
using System.Linq;

public class LineDrawScript : MonoBehaviour
{
    /* Known Bugs:
    Singularities
    Square Root
    Logaritmes
    Fraction Exponents
    ** soms --> * ipv ^
    */

    public int amountOfPoints = 50;
    public float xStart = -5f, xEnd = 5f, lineWidth = 0.1f, hitBoxThickness = 0.2f;
    public string function = "2x²";
    public LineRenderer myLineRenderer;
    public EdgeCollider2D myEdgeCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // variables
        myLineRenderer.positionCount = amountOfPoints;
        myLineRenderer.startWidth = lineWidth;
        myLineRenderer.endWidth = lineWidth;
        Vector2[] colliderpoints = new Vector2[amountOfPoints];

        int counter;
        float positionX, positionY, step;
        char[] operators = {'+', '-', '*', 'x', '/', '(', ')'};
        
        // calculates the amount of space between each point
        step = (xEnd - xStart) / (amountOfPoints -1);

        /* Clean up user input */
        // trimp function
        if(function.Contains(" ")){
            function = Regex.Replace(function, " ", "");
        }

        // translates superscript to normal letters if needed
        if (function.Any(symbol => new char[] {'⁰', '¹', '²', '³', '⁴', '⁵', '⁶', '⁷', '⁸', '⁹'}.Contains(symbol))){
            function = superscriptTranslation(function);
        }

        // adds 1 before x if needed
        if (function.IndexOf("x") == 0 || (operators.Contains(function[function.IndexOf("x")-1]) && (function.IndexOf("x")-1).Equals("*"))){
            if (function.IndexOf("x") == 0){
                function = "1" + function;
            }
            else{
                string[] templist = function.Split("x");
                function = templist[0] + "1x" + templist[1];
            }
        }

        // checks if there should be an * in front of the x
        if (function.IndexOf("x") > 0 && !operators.Contains(function[function.IndexOf("x")-1])){
            string[] templist = function.Split("x");
            function = templist[0] + "*x" + templist[1];
        }

        // remove · and replace with *
        if (function.Contains("·")){
            function = Regex.Replace(function, "·", "*");
        }

        // removes **
        if (function.Contains("**")){
            function = Regex.Replace(function, "\\*\\*", "*");
        }

        // draw line and put them in a list
        for (counter = 0; counter < amountOfPoints; counter++){
            // determine the x position for the next point
            positionX = xStart + counter * step;
            Debug.Log(positionX);
            
            // determine the y position for the next point
            var parser = new MathParser();
            parser.LocalVariables["x"] = positionX;
            positionY = Convert.ToSingle(parser.Parse(function));
            Debug.Log(positionY);

            // draws the point and adds it to a list
            Vector3 point = new Vector3(positionX, positionY, 0);
            myLineRenderer.SetPosition(counter, point);
            colliderpoints[counter] = new Vector2(positionX, positionY);

            /* Debug.Log("End of iteration. ---------"); */
        }

        // add hitbox
        myEdgeCollider.points = colliderpoints;
    }

    // Update is called once per frame
    void Update()
    {
        // not needed for now
    }

    private String superscriptTranslation(String givenFunction){
        // needs to be improved to account for superscript possibly being multiple digits long, but for now it will suffice
        foreach (char symbol in givenFunction){
            switch (symbol){
                case '⁰': givenFunction = Regex.Replace(givenFunction, '⁰'.ToString(), "^0"); break;
                case '¹': givenFunction = Regex.Replace(givenFunction, '¹'.ToString(), "^1"); break;
                case '²': givenFunction = Regex.Replace(givenFunction, '²'.ToString(), "^2"); break;
                case '³': givenFunction = Regex.Replace(givenFunction, '³'.ToString(), "^3"); break;
                case '⁴': givenFunction = Regex.Replace(givenFunction, '⁴'.ToString(), "^4"); break;
                case '⁵': givenFunction = Regex.Replace(givenFunction, '⁵'.ToString(), "^5"); break;
                case '⁶': givenFunction = Regex.Replace(givenFunction, '⁶'.ToString(), "^6"); break;
                case '⁷': givenFunction = Regex.Replace(givenFunction, '⁷'.ToString(), "^7"); break;
                case '⁸': givenFunction = Regex.Replace(givenFunction, '⁸'.ToString(), "^8"); break;
                case '⁹': givenFunction = Regex.Replace(givenFunction, '⁹'.ToString(), "^9"); break;
            }
        }

        return givenFunction;
    }
}
